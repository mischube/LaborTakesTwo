using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Weapon;

public class WeaponRod : WeaponScript
{
    //will be loaded in particular script
    protected GameObject projectilePrefab;
    protected GameObject beamPrefab;
    
    protected PhotonParticle photonParticle;
    protected GameObject invisBeam;

    protected void Start()
    {
        photonParticle = GetComponent<PhotonParticle>();
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //set particular particle eg. photonParticle.fireParticle = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = CompTransform.position;

        invisBeam = Instantiate(
            beamPrefab,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * beamPrefab.transform.rotation);
        invisBeam.transform.parent = transform;
        //set the name in get Particle eg. invisBeam.GetComponent<GetParticle>().rodName = "Fire";
    }

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(projectilePrefab.name, transform.position, transform.rotation);
    }
}
