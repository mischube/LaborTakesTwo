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
        private GameObject weaponHolder;

        private void Start()
        {
            weaponHolder = transform.GetChild(0).gameObject;
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
                SwitchWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeapon(-1);
            }
        }


        private void PickupWeapon()
        {
            var newWeaponObj = _interactableInfo.GetHoveredItem();

            if (newWeaponObj == null)
                return;

            var newWeapon = newWeaponObj.GetComponent<WeaponScript>();

            //clone script
            var newWeaponClone = weaponHolder.AddComponent(newWeapon.GetType()) as WeaponScript;
            newWeaponClone!.weaponContainer = newWeapon.weaponContainer;

            //integrate in inventory
            newWeaponClone.enabled = false;
            _weapons.AddLast(newWeaponClone);

            Destroy(newWeapon.gameObject);
        }

        private void SwitchWeapon(int i)
        {
            activeWeapon.enabled = false;
            
            if (i > 0)
            {
                var nextWeapon = _weapons.Find(activeWeapon)!.Next;
                if (nextWeapon is null)
                {
                    activeWeapon = _weapons.First.Value;
                }
                else
                {
                    activeWeapon = nextWeapon!.Value;
                }
            }
            else if (i < 0)
            {
                var nextWeapon = _weapons.Find(activeWeapon)!.Previous;
                if (nextWeapon is null)
                {
                    activeWeapon = _weapons.Last.Value;
                }
                else
                {
                    activeWeapon = nextWeapon!.Value;
                }
            }

            activeWeapon.enabled = true;
        }
    }
}