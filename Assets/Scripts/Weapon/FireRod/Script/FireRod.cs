using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class FireRod : WeaponScript
{
    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    public override void SecondaryAction()
    {
        Debug.Log("rechts");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
}
