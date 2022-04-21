using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int selectedWeapon;
        [SerializeField] private GameObject uiEnabler;

        private Transform _baseWeaponPosition;
        private InteractableInfo _interactable;
        private bool _onInit;
        private GameObject _pickUpWeapon;
        private List<Transform> _weapons = new List<Transform>();

        private void Update()
        {
            PickupWeapon();
            int oldWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (oldWeapon != selectedWeapon)
                SelectWeapon();
        }

        private void PickupWeapon()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var inventory = uiEnabler.GetComponent<UIVisibility>();
                _interactable = transform.parent.gameObject.GetComponent<InteractableInfo>();

                if (!_onInit && _interactable.itemHoveredNow)
                {
                    foreach (Transform weapon in transform)
                    {
                        _weapons.Add(weapon);
                    }

                    SetHandPosition();
                    inventory.EnableAllUI();
                    _onInit = true;
                }

                if (_interactable.itemHoveredNow)
                {
                    _pickUpWeapon = _interactable.GetHoveredItem();
                    _weapons.Add(_pickUpWeapon.transform);
                    _pickUpWeapon.transform.SetParent(transform);
                    _pickUpWeapon.layer = 0;
                    _pickUpWeapon.transform.position = _baseWeaponPosition.transform.position;
                    ClearHands();
                    _pickUpWeapon.gameObject.SetActive(true);
                    if (_weapons.Count == 3)
                        inventory.TurnUIElementsAround(2);
                    if (_weapons.Count == 4)
                        inventory.TurnUIElementsAround(3);
                }
            }
        }

        private void SelectWeapon()
        {
            var inventory = uiEnabler.GetComponent<UIVisibility>();
            int i = 0;
            foreach (Transform weapon in _weapons)
            {
                if (i == selectedWeapon)
                {
                    inventory.TurnUIElementsAround(i);
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }

                i++;
            }
        }

        private void ClearHands()
        {
            foreach (Transform weapon in _weapons)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        private void SetHandPosition()
        {
            _baseWeaponPosition = _weapons[0];
        }
    }
}