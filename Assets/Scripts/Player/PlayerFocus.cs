using UnityEngine;

namespace Player
{
    public class PlayerFocus : MonoBehaviour
    {
        public Transform cam;
        public Transform focus;

        public Vector3 GetViewDirection()
        {
            return (focus.position - cam.position).normalized;
        }
    }
}
