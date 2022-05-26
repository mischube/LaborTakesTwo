using Photon.Pun;
using UnityEngine;
using Weapon;

public class FireRod : WeaponScript
{
    private GameObject fireProjectilePrefab;
    private PhotonParticel photonParticel;

    private void Start()
    {
        photonParticel = GetComponent<PhotonParticel>();
        photonParticel.firerod = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        //load prefab
        fireProjectilePrefab = (GameObject)Resources.Load("FireProjectile", typeof(GameObject));
    }

    public override void PrimaryAction()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        photonParticel.fireParticel = true;
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
        }
    }
}
