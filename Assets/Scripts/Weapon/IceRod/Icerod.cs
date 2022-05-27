using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    private GameObject iceProjectilePrefab;
    private PhotonParticel photonParticel;
    private GameObject iceBeam;
    private GameObject photonBeam;

    private void Start()
    {
        //load prefab
        iceProjectilePrefab = (GameObject)Resources.Load("IceProjectile", typeof(GameObject));
        iceBeam = (GameObject)Resources.Load("Beam", typeof(GameObject));
        
        photonParticel = GetComponent<PhotonParticel>();
        photonParticel.icerod = this;
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticel.iceParticel = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = GetComponentInParent<Transform>().position;

        // photonBeam = PhotonNetwork.Instantiate(iceBeam.name,
        //     position + new Vector3(CompTransform.forward.x * 7.5f, 0 , CompTransform.forward.z * 7.5f), 
        //     transform.rotation * iceBeam.transform.rotation);
        // photonBeam.transform.parent = transform;

        photonBeam = Instantiate(
            iceBeam,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * iceBeam.transform.rotation);
        photonBeam.transform.parent = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            photonParticel.iceParticel = false;
            Destroy(photonBeam);
        }
    }

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(iceProjectilePrefab.name, transform.position, transform.rotation);
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.layer == 15)
    //     {
    //         transform.GetChild(2).gameObject.SetActive(true);
    //         transform.GetChild(2).gameObject.transform.position = other.transform.position;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.layer == 15)
    //     {
    //         transform.GetChild(2).gameObject.SetActive(false);
    //     }
    // }
}
