using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponRod
{
    private new void Start()
    {
        base.Start();
        
        //load prefab
        projectilePrefab = (GameObject)Resources.Load("IceProjectile");
        beamPrefab = (GameObject)Resources.Load("BeamIce");
    }

    public override void PrimaryAction()
    {
        base. PrimaryAction();
        
        photonParticle.iceParticle = true;
        
        invisBeam.GetComponent<GetParticle>().rodName = "Ice";
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            photonParticle.iceParticle = false;
            Destroy(invisBeam);
        }
    }
}
