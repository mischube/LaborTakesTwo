using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class Icerod : WeaponScript
{
    private GameObject iceProjectilePrefab;
    private PhotonParticel photonParticel;
    private GameObject beamPrefab;
    private GameObject invisBeam;

    private void Start()
    {
        //load prefab
        iceProjectilePrefab = (GameObject)Resources.Load("IceProjectile", typeof(GameObject));
        beamPrefab = (GameObject)Resources.Load("BeamIce", typeof(GameObject));
        
        photonParticel = GetComponent<PhotonParticel>();
        photonParticel.icerod = this;
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticel.iceParticel = true;
        
        Transform CompTransform = GetComponentInParent<Transform>();
        Vector3 position = GetComponentInParent<Transform>().position;

        invisBeam = Instantiate(
            beamPrefab,
            position + new Vector3(CompTransform.forward.x * 7.5f, 0, CompTransform.forward.z * 7.5f),
            transform.rotation * beamPrefab.transform.rotation);
        invisBeam.transform.parent = transform;
        invisBeam.GetComponent<GetParticel>().rodName = "Ice";
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            photonParticel.iceParticel = false;
            Destroy(invisBeam);
        }
    }

    public override void SecondaryAction()
    {
        PhotonNetwork.Instantiate(iceProjectilePrefab.name, transform.position, transform.rotation);
    }
}
