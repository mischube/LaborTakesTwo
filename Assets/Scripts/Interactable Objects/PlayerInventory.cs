using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Transform> _weapons = new List<Transform>();
    private Transform _baseWeaponPosition;
    public int _selectedWeapon= 0;
    private InteractableInfo _interactable;
    private GameObject _pickUpWeapon;
    private bool onInit = false;
    private void PickupWeapon()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!onInit)
            {
                foreach (Transform weapon in transform)
                {
                    _weapons.Add(weapon);
                }
                SetHandPosition();
                onInit = true;
            }
            _interactable = transform.parent.gameObject.GetComponent<InteractableInfo>();
            if (_interactable.itemHoveredNow)
            {
                _pickUpWeapon = _interactable.GetHoveredItem();
                _weapons.Add(_pickUpWeapon.transform);
                _pickUpWeapon.transform.SetParent(transform);
                _pickUpWeapon.layer = 0;
                _pickUpWeapon.transform.position = _baseWeaponPosition.transform.position;
                ClearHands();
                _pickUpWeapon.gameObject.SetActive(true);
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
        int i = 0;
        foreach (Transform weapon  in _weapons)
        {
            if (i == _selectedWeapon)
            {
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
