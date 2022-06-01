using Photon.Pun;
using UnityEngine;

public class FireRod : WeaponRod
{
    private new void Start()
    {
        base.Start();
        
        //load prefab
        projectilePrefab = (GameObject)Resources.Load("FireProjectile");
        beamPrefab = (GameObject)Resources.Load("BeamFire");
    }

    public override void PrimaryAction()
    {
        base. PrimaryAction();
        
        photonParticle.fireParticle = true;
        
        invisBeam.GetComponent<GetParticle>().rodName = "Fire";
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
