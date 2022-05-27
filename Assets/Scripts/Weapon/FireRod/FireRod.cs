using System;
using Photon.Pun;
using UnityEngine;
using Weapon;

public class FireRod : WeaponScript
{
    private GameObject fireProjectilePrefab;
    private PhotonParticel photonParticel;
    private GameObject fireBeam;
    private GameObject photonBeam;

    private void Start()
    {
        //load prefab
        fireProjectilePrefab = (GameObject)Resources.Load("FireProjectile", typeof(GameObject));
        fireBeam = (GameObject)Resources.Load("Beam", typeof(GameObject));
        
        photonParticel = GetComponent<PhotonParticel>();
        photonParticel.firerod = this;
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticel.fireParticel = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = GetComponentInParent<Transform>().position;

        photonBeam = Instantiate(
            fireBeam,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * fireBeam.transform.rotation);
        photonBeam.transform.parent = transform;
    }

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(fireProjectilePrefab.name, transform.position, transform.rotation);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            photonParticel.fireParticel = false;
            Destroy(photonBeam);
        }
    }
}
