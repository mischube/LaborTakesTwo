using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    private GameObject iceProjectilePrefab;
    private PhotonParticel photonParticel;

    private void Start()
    {
        photonParticel = GetComponent<PhotonParticel>();
        photonParticel.icerod = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        //load prefab
        iceProjectilePrefab = (GameObject)Resources.Load("IceProjectile", typeof(GameObject));
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticel.iceParticel = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            photonParticel.iceParticel = false;
        }
    }

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(iceProjectilePrefab.name, transform.position, transform.rotation);
    }
}
