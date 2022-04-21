using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class TextBoxScript : MonoBehaviour
{
    public TextMesh interactionText;

    private WeaponScript _weapon;

    private void Start()
    {
        _weapon = GetComponent<WeaponScript>();
    }

    public string GetDescription()
    {
        return "Press <color=green>[F]</color> to Pickup";
    }

    public void SetTextRotation(Quaternion rotation)
    {
        interactionText.transform.rotation = rotation;
    }

    public void ShowDescription()
    {
        interactionText.text = _weapon.weaponContainer.description;
    }

    public void HideDescription()
    {
        interactionText.text = "";
    }
}