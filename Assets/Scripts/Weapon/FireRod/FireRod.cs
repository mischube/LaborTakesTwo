using System;
using Photon.Pun;
using UnityEngine;
using Weapon;

public class FireRod : WeaponScript
{
    private GameObject fireProjectilePrefab;
    private PhotonParticle photonParticle;
    private GameObject fireBeamPrefab;
    private GameObject invisBeam;

    private void Start()
    {
        //load prefab
        fireProjectilePrefab = (GameObject)Resources.Load("FireProjectile");
        fireBeamPrefab = (GameObject)Resources.Load("BeamFire");
        
        photonParticle = GetComponent<PhotonParticle>();
        photonParticle.firerod = this;
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticle.fireParticle = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = CompTransform.position;

        invisBeam = Instantiate(
            fireBeamPrefab,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * fireBeamPrefab.transform.rotation);
        invisBeam.transform.parent = transform;
        invisBeam.GetComponent<GetParticle>().rodName = "Fire";
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
            photonParticle.fireParticle = false;
            Destroy(invisBeam);
        }
    }
}
