using UnityEngine;

namespace Door
{
    public class DoorSwitch : MonoBehaviour
    {
        [SerializeField] private Door door;
        [SerializeField] private bool closeOnLeave;

        private void OnTriggerEnter(Collider other)
        {
            door.Open();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!closeOnLeave)
            {
                return;
            }

            door.Close();
        }
    }
}
