using Player;
using UnityEngine;
using Weapon;

public class WateringCan : WeaponScript
{
    [SerializeField] private ParticleSystem particleSystem;
    private Vector3 takeoverCenter;
    private float takeoverRadius = 1f;
    private GameObject player;
    private GameObject plantExtensionPrefab;
    private SnakeMovement snakeMovement;
    private CharacterController cc;
    private Transform groundcheck;
    private bool polymorphGrownActive;
    private bool polymorphSnakeActive;
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

        if (!polymorphGrownActive)
        {
            groundcheck = player.transform.Find("GroundCheck");
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Grown") && !polymorphGrownActive)
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

            if (hitCollider.gameObject.CompareTag("GrownSnake") && !polymorphGrownActive && !polymorphSnakeActive)
            {
                int lastChild = hitCollider.transform.childCount - 1;
                currentPlant = hitCollider.transform.GetChild(lastChild);
                plantExtensionPrefab = hitCollider.transform.GetChild(lastChild).gameObject;
                EnableSnakePolymorph();

                oldPlantPosition = player.transform.position; //Care its used for the old player pos this time
                oldPlantParent = hitCollider.gameObject;
                player.transform.position = hitCollider.transform.GetChild(lastChild).position;
                currentPlant.SetParent(player.transform);
                polymorphSnakeActive = true;
                return;
            }
        }

        if (polymorphGrownActive)
        {
            resetCC();
            DisablePolymorph();
            var minY = player.transform.Find("Cylinder").GetComponent<Renderer>().bounds.min.y;
            currentPlant.SetParent(oldPlantParent.transform);
            currentPlant.position = oldPlantPosition;
            groundcheck.position = new Vector3(groundcheck.position.x, minY, groundcheck.position.z);
            cc.enabled = true;
        }

        if (polymorphSnakeActive)
        {
            DisablePolymorph();
            currentPlant.SetParent(oldPlantParent.transform);
            player.transform.position = oldPlantPosition;
            player.transform.GetComponent<PlayerMovement>().enabled = true;
            cc.enabled = true;
        }
    }

    private void EnableSnakePolymorph()
    {
        cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.Find("Cylinder").gameObject.SetActive(false);
        player.transform.Find("Cube").gameObject.SetActive(false);
        player.transform.Find("Inventory").gameObject.SetActive(false);
        player.transform.Find("GroundCheck").gameObject.SetActive(false);
        player.transform.GetComponent<PlayerMovement>().enabled = false;
        polymorphSnakeActive = true;
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
        polymorphGrownActive = true;
    }

    private void resetCC()
    {
        cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        cc.center = oldCharacterControllerCenter;
        cc.height = oldCharacterControllerHeight;
    }

    private void DisablePolymorph()
    {
        player.transform.Find("Cylinder").gameObject.SetActive(true);
        player.transform.Find("Cube").gameObject.SetActive(true);
        player.transform.Find("Inventory").gameObject.SetActive(true);
        player.transform.Find("GroundCheck").gameObject.SetActive(true);
        polymorphGrownActive = false;
        polymorphSnakeActive = false;
    }
}
