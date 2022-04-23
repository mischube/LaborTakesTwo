using System.Collections.Generic;
using UI;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public WeaponScript activeWeapon;

        private readonly LinkedList<WeaponScript> _weapons = new LinkedList<WeaponScript>();
        private InteractableInfo _interactableInfo;
        private GameObject _weaponHolder;

        private void Start()
        {
            _weaponHolder = transform.GetChild(0).gameObject;
            _interactableInfo = GetComponentInParent<InteractableInfo>();
            _weapons.AddLast(gameObject.GetComponentInChildren<WeaponScript>());
            activeWeapon = _weapons.First.Value;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickupWeapon();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchWeapon(true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeapon(false);
            }
        }


        private void PickupWeapon()
        {
            var newWeaponObj = _interactableInfo.GetHoveredItem();

            if (newWeaponObj == null)
                return;

            var newWeapon = newWeaponObj.GetComponent<WeaponScript>();

            //clone script
            var newWeaponClone = _weaponHolder.AddComponent(newWeapon.GetType()) as WeaponScript;
            newWeaponClone!.weaponContainer = newWeapon.weaponContainer;

            //integrate in inventory
            newWeaponClone.enabled = false;
            _weapons.AddLast(newWeaponClone);

            Destroy(newWeapon.gameObject);
        }

        private void SwitchWeapon(bool stepForward)
        {
            activeWeapon.enabled = false;
            var nextWeapon = _weapons.Find(activeWeapon)!.Next;

            if (nextWeapon == null)
                nextWeapon = stepForward ? _weapons.First : _weapons.Last;

            activeWeapon = nextWeapon!.Value;
            activeWeapon.enabled = true;
        }
    }
}