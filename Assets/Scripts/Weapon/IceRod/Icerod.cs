using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    private GameObject iceProjectilePrefab;
    private PhotonParticle photonParticle;
    private GameObject beamPrefab;
    private GameObject invisBeam;

    private void Start()
    {
        //load prefab
        iceProjectilePrefab = (GameObject)Resources.Load("IceProjectile");
        beamPrefab = (GameObject)Resources.Load("BeamIce");
        
        photonParticle = GetComponent<PhotonParticle>();
        photonParticle.icerod = this;
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticle.iceParticle = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = CompTransform.position;

        invisBeam = Instantiate(
            beamPrefab,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * beamPrefab.transform.rotation);
        invisBeam.transform.parent = transform;
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

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(iceProjectilePrefab.name, transform.position, transform.rotation);
    }
}
