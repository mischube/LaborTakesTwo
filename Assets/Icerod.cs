using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    public override void PrimaryAction()
    {
        Debug.Log("Rechts");
    }

    public override void SecondaryAction()
    {
        Debug.Log("Links");
    }
}
