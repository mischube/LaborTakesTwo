using UnityEngine;
using Weapon;

public class WateringCan : WeaponScript
{
    [SerializeField] private ParticleSystem particleSystem;
    private Vector3 takeoverCenter;
    private float takeoverRadius = 1f;
    private GameObject player;
    private CharacterController cc;
    private Transform groundcheck;
    private bool polymorphActive;

    private Vector3 characterControllerCenterOffset;
    private float characterControllerHeightOffset;

    private Vector3 oldPlantPosition;
    private GameObject oldPlantParent;
    private Transform currentPlant;
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
        takeoverCenter = player.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(takeoverCenter, takeoverRadius);

        if (!polymorphActive)
        {
            groundcheck = player.transform.Find("GroundCheck");
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Grown") && !polymorphActive)
            {
                currentPlant = hitCollider.transform.GetChild(0);
                EnablePolymorph();

                oldPlantPosition = hitCollider.transform.GetChild(0).position;
                var minY = hitCollider.transform.GetChild(0).GetComponent<Renderer>().bounds.min.y;
                oldPlantParent = hitCollider.gameObject;
                player.transform.position = hitCollider.transform.GetChild(0).position;
                hitCollider.transform.GetChild(0).SetParent(player.transform);
                groundcheck.position = new Vector3(player.transform.position.x, minY, player.transform.position.z);
                cc.enabled = true;
                return;
            }

            if (hitCollider.GetComponent<PlantType>().getPlantType().Equals("Growable") && !polymorphActive)
            {
                Debug.Log(hitCollider.GetComponent<PlantType>().getPlantType());
            }
        }

        if (polymorphActive)
        {
            DisablePolymorph();
            var minY = player.transform.Find("Cylinder").GetComponent<Renderer>().bounds.min.y;
            currentPlant.SetParent(oldPlantParent.transform);
            currentPlant.position = oldPlantPosition;
            groundcheck.position = new Vector3(groundcheck.position.x, minY, groundcheck.position.z);
            cc.enabled = true;
        }
    }

    private void EnablePolymorph()
    {
        cc = player.GetComponent<CharacterController>();

        oldCharacterControllerCenter = cc.center;
        oldCharacterControllerHeight = cc.height;

        characterControllerCenterOffset.y = -currentPlant.GetComponent<Renderer>().bounds.size.y / 2.2f;
        characterControllerHeightOffset = currentPlant.localScale.x * 5.15f;
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
