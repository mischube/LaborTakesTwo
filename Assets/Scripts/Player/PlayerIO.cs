using UnityEngine;

namespace Player
{
    public class PlayerIO : MonoBehaviour
    {
        [SerializeField] private PlayerInventory playerInventory;

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
        }
    }
}