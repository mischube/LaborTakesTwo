using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    private GameObject iceProjectilePrefab;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        //load prefab
        iceProjectilePrefab = (GameObject)Resources.Load("IceProjectile", typeof(GameObject));
    }

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
        PhotonNetwork.Instantiate(iceProjectilePrefab.name, transform.position, transform.rotation);
    }
}
