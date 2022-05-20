using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    public override void SecondaryAction()
    {
        Debug.Log("Rechts");
    }
}
