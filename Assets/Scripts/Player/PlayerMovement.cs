using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        //All Controller and Objects
    public CharacterController characterController;
    public GameObject cam;
    
    //all public variables
    public Transform groundCheck;
    public LayerMask groundMask;

    //general variables
    private bool isGrounded;
    private float groundDistance = 0.4f;
    private float speed = 12f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    
    //jump variables
    private bool doubleJumpAvailable;
    private float jumpHeight = 3f;
    
    //dash variables
    [SerializeField] public float dashSpeed = 200f;
    private bool dashActive = false;
    private float dashCooldown = 3f;
    private float dashTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        //Check if on Ground (could be replaced with charactercontroller.isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //Get Player Inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        
        //move in Direction
        Vector3 moveDirection = cam.transform.right * x + cam.transform.forward * z;
        moveDirection.y = 0f;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        //rotating player
        //only rotate if player really moves
        if (moveDirection.magnitude > 0.1f)
        {
            //calculate degree with tan through x vector and z vector
            float rotationDegree = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            
            //Quaternion.Euler uses the degree value to transform it into an quaternion
            transform.rotation = Quaternion.Euler(0,rotationDegree,0);
        }

        Jump();
        Dash();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            doubleJumpAvailable = true;
        }
        
        
        if (Input.GetButtonDown("Jump") && (isGrounded || doubleJumpAvailable))
        {
            //general gravity formula
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (!isGrounded)
            {
                doubleJumpAvailable = false;
            }
        }
        
        //let the player fall
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
    }
    
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashActive)
        {
            dashActive = true;
            StartCoroutine(Dashing());
            StartCoroutine(DashCoolDown());
        }
    }
    
    IEnumerator Dashing()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashActive = false;
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * 2f;
    }
    }
}