using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        //all public variables
        public CharacterController characterController;
        public GameObject cam;
        public Transform groundCheck;
        public LayerMask groundMask;

        //Serialize fields
        [SerializeField] private float speed = 12f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float dashSpeed = 200f;
        [SerializeField] private float dashCooldown = 3f;
        [SerializeField] private float dashTime = 0.5f;
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private float groundDistance = 0.4f;

        //general variables
        private bool _isGrounded;
        private Vector3 _velocity;
        private bool _doubleJumpAvailable;
        private bool _dashActive;


        void Update()
        {
            if (!photonView.IsMine)
                return;

            //Check if on Ground (could be replaced with charactercontroller.isGrounded
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            //Get Player Inputs
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");


            //move in Direction
            Vector3 moveDirection = cam.transform.right * x + cam.transform.forward * z;
            moveDirection.y = 0f;
            characterController.Move(moveDirection * (speed * Time.deltaTime));

            //rotating player
            //only rotate if player really moves
            if (moveDirection.magnitude > 0.1f)
            {
                //calculate degree with tan through x vector and z vector
                float rotationDegree = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                //Quaternion.Euler uses the degree value to transform it into an quaternion
                transform.rotation = Quaternion.Euler(0, rotationDegree, 0);
            }

            Jump();
            Dash();
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                _doubleJumpAvailable = true;
            }


            if (Input.GetButtonDown("Jump") && (_isGrounded || _doubleJumpAvailable))
            {
                //general gravity formula
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (!_isGrounded)
                {
                    _doubleJumpAvailable = false;
                }
            }

            //let the player fall
            _velocity.y += gravity * Time.deltaTime;
            characterController.Move(_velocity * Time.deltaTime);
        }

        private void Dash()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) &&
                !_dashActive)
            {
                _dashActive = true;
                StartCoroutine(Dashing());
                StartCoroutine(DashCoolDown());
            }
        }

        private IEnumerator Dashing()
        {
            var startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                characterController.Move(transform.forward * (dashSpeed * Time.deltaTime));
                yield return null;
            }
        }

        private IEnumerator DashCoolDown()
        {
            yield return new WaitForSeconds(dashCooldown);
            _dashActive = false;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null ||
                body.isKinematic)
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
