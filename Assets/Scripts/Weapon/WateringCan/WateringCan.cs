using UnityEngine;
using Weapon;

public class WateringCan : WeaponScript
{
    [SerializeField] private ParticleSystem particleSystem;
    private Vector3 takeoverCenter;
    private float takeoverRadius = 1f;
    private GameObject player;
    private CharacterController cc;

    private bool polymorphActive;

    private Vector3 characterControllerCenterOffset = new Vector3(0, -3, 0);
    private float characterControllerHeightOffset = 5f;

    private Vector3 oldPlantPosition;
    private GameObject oldPlantParent;
    private Transform currentPlant;
    private Vector3 oldGroundCheckPosition;
    private Vector3 oldCharacterControllerCenter;
    private float oldCharacterControllerHeight;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            particleSystem.Stop();
    }

    public override void PrimaryAction()
    {
        particleSystem = transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    public override void SecondaryAction()
    {
        player = transform.root.gameObject;
        Transform groundcheck = player.transform.Find("GroundCheck");
        takeoverCenter = player.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(takeoverCenter, takeoverRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Grown") && !polymorphActive)
            {
                EnablePolymorph();

                oldPlantPosition = hitCollider.transform.GetChild(0).position;
                oldGroundCheckPosition = groundcheck.position;
                currentPlant = hitCollider.transform.GetChild(0);
                oldPlantParent = hitCollider.gameObject;

                player.transform.position = hitCollider.transform.GetChild(0).position;
                groundcheck.position = new Vector3(player.transform.position.x, -0.5f, player.transform.position.z);

                hitCollider.transform.GetChild(0).SetParent(player.transform);
                cc.enabled = true;
                return;
            }
        }

        if (polymorphActive)
        {
            DisablePolymorph();

            currentPlant.SetParent(oldPlantParent.transform);
            currentPlant.position = oldPlantPosition;
            groundcheck.position = oldGroundCheckPosition;
            cc.enabled = true;
        }
    }

    private void EnablePolymorph()
    {
        cc = player.GetComponent<CharacterController>();

        oldCharacterControllerCenter = cc.center;
        oldCharacterControllerHeight = cc.height;

        cc.enabled = false;
        cc.center = characterControllerCenterOffset;
        cc.height = characterControllerHeightOffset;

        player.transform.Find("Cylinder").gameObject.SetActive(false);
        player.transform.Find("Cube").gameObject.SetActive(false);
        player.transform.Find("Inventory").gameObject.SetActive(false);
        polymorphActive = true;
    }

    private void DisablePolymorph()
    {
        cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        cc.center = oldCharacterControllerCenter;
        cc.height = oldCharacterControllerHeight;
        player.transform.Find("Cylinder").gameObject.SetActive(true);
        player.transform.Find("Cube").gameObject.SetActive(true);
        player.transform.Find("Inventory").gameObject.SetActive(true);
        polymorphActive = false;
    }
}
