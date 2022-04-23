using Player;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace UI
{
    public class WeaponUI : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public Image weaponIconUIHolder;

        private WeaponScript _activeWeapon;

        private void Start()
        {
            //playerInventory = Player.Player.LocalPlayerInstance.GetComponent<PlayerInventory>();
            UpdateIcon();
        }

        private void Update()
        {
            if (_activeWeapon != playerInventory.activeWeapon)
            {
                UpdateIcon();
            }
        }

        private void UpdateIcon()
        {
            _activeWeapon = playerInventory.activeWeapon;
            weaponIconUIHolder.sprite = _activeWeapon.weaponContainer.icon;
        }
    }
}