using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        public CharacterController characterController;
        public float speed = 6f;
        public Camera cam;
        public float turnSmoothTime;

        private float _turnSmoothVelocity;
        private float _gravityVelocity = 0.1f;


        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine)
                return;

            ApplyGravity();

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            
            if (!(direction.magnitude >= 0.1f))
                return;
            
            MoveCharacter(direction);
        }

        private void MoveCharacter(Vector3 direction)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
            {
                _gravityVelocity += 0.1f;
                characterController.Move(new Vector3(0f, -_gravityVelocity * Time.deltaTime, 0f));
            }
            else
            {
                _gravityVelocity = 0.1f;
            }
        }
    }
}