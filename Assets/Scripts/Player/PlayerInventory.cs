using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private List<Transform> _weapons = new List<Transform>();
        private Transform _baseWeaponPosition;
        [SerializeField]
        private int _selectedWeapon= 0;
        private InteractableInfo _interactable;
        [SerializeField]
        private GameObject _uiEnabler;
        private GameObject _pickUpWeapon;
        private bool onInit = false;
        private void PickupWeapon()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var inventory = _uiEnabler.GetComponent<UIVisibility>();
                _interactable = transform.parent.gameObject.GetComponent<InteractableInfo>();
                if (!onInit && _interactable.itemHoveredNow)
                {
                    foreach (Transform weapon in transform)
                    {
                        _weapons.Add(weapon);
                    }
                    SetHandPosition();
                    inventory.EnableAllUI();
                    onInit = true;
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
                    if(_weapons.Count == 3)
                        inventory.TurnUIElementsAround(2);
                    if(_weapons.Count == 4)
                        inventory.TurnUIElementsAround(3);
                }
            }
        }
    
        private void Update()
        {
            PickupWeapon();
            int oldWeapon = _selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (_selectedWeapon >= transform.childCount - 1)
                    _selectedWeapon = 0;
                else
                    _selectedWeapon++;
            }
        
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (_selectedWeapon <= 0)
                    _selectedWeapon = transform.childCount - 1;
                else
                    _selectedWeapon--;
            }
            if(oldWeapon != _selectedWeapon)
                SelectWeapon();
        }

        private void SelectWeapon()
        {
            var inventory = _uiEnabler.GetComponent<UIVisibility>();
            int i = 0;
            foreach (Transform weapon  in _weapons)
            {
                if (i == _selectedWeapon)
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
