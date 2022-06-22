using UnityEngine;

namespace Player
{
    public class PlayerIO : MonoBehaviour
    {
        [SerializeField] private PlayerInventory playerInventory;

        private bool _isLocked = true;

        private void Start()
        {
            playerInventory = GetComponentInChildren<PlayerInventory>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerInventory.activeWeapon.PrimaryAction();
            }

            if (Input.GetMouseButtonDown(1))
            {
                playerInventory.activeWeapon.SecondaryAction();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                ToggleMouse();
            }
        }

        private void ToggleMouse()
        {
            if (_isLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _isLocked = false;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _isLocked = true;
            }
        }
    }
}
