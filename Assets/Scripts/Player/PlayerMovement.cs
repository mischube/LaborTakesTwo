using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
    {
        public CharacterController characterController;
        public float speed = 6f;
        public Camera camera;
        public float turnSmoothTime;

        private float _turnSmoothVelocity;


        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine)
            {
                camera.enabled = false;
                return;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            
            if (!(direction.magnitude >= 0.1f))
                return;
            
            MoveCharacter(direction);
        }

        private void MoveCharacter(Vector3 direction)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}