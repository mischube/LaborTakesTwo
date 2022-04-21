using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace UI
{
    public class WeaponUI : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public Image weaponIcon;

        private WeaponScript _activeWeapon;

        private void Start()
        {
            //playerInventory = Player.Player.LocalPlayerInstance.GetComponent<PlayerInventory>();
        }

        private void Update()
        {
            if (_activeWeapon != playerInventory.activeWeapon)
            {
                //Update icon
                _activeWeapon = playerInventory.activeWeapon;
                weaponIcon.sprite = _activeWeapon.weaponContainer.icon;
            }
        }
    }
}
