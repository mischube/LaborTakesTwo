using Photon.Pun;
using UnityEngine;
using Weapon;

public class WateringCan : WeaponScript
{
    [SerializeField] private ParticleSystem particleSystem;
    private PhotonParticle photonParticle;
    private CharacterController cc;

    private GameObject player;
    private GameObject plantExtensionPrefab;
    private GameObject oldPlantParent;

    private bool polymorphGrownActive;
    private bool polymorphSnakeActive;

    private Vector3 characterControllerCenterOffset;
    private Vector3 oldCharacterControllerCenter;
    private Vector3 oldPlantPosition;
    private Vector3 takeoverCenter;

    private Transform currentPlant;
    private Transform groundcheck;

    private float takeoverRadius = 1.5f;
    private float characterControllerHeightOffset;
    private float oldCharacterControllerHeight;

    private void Start()
    {
        photonParticle = GetComponent<PhotonParticle>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            particleSystem.Stop();
            photonParticle.wateringParticle = false;
        }
    }

    public override void PrimaryAction()
    {
        particleSystem = transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
        particleSystem.Play();
        photonParticle.wateringParticle = true;
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

        //Schaut alle Objekte durch die von OverlapSpehere erfasst wurden sind und aktiviert jenachdem ob eine
        //Pflanze gefunden wurde die nötige Polymorph
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Grown") && !polymorphGrownActive)
            {
                currentPlant = hitCollider.transform.GetChild(0);
                EnableGrowablePlantPolymorph();

                oldPlantPosition = hitCollider.transform.GetChild(0).position;
                var minY = hitCollider.transform.GetChild(0).GetComponent<Renderer>().bounds.min.y;
                oldPlantParent = hitCollider.gameObject;

                player.transform.position = hitCollider.transform.GetChild(0).position;
                hitCollider.transform.GetChild(0).SetParent(player.transform);
                groundcheck.position = new Vector3(player.transform.position.x, minY, player.transform.position.z);
                cc.enabled = true;

                player.GetComponent<PhotonPlant>().SetPolyPlant(currentPlant.position);
                player.GetComponent<PhotonPlant>().SetPlantSize(currentPlant.localScale);
                player.GetComponent<PhotonPlant>().SetPolyEnable(true);
                player.GetComponent<PhotonPlant>().SetWhichPlant(false);
                player.GetComponent<PhotonPlant>().SetPolyOwner();
                return;
            }

            if (hitCollider.gameObject.CompareTag("GrownSnake") && !polymorphGrownActive && !polymorphSnakeActive)
            {
                int lastChild = hitCollider.transform.childCount - 1;
                currentPlant = hitCollider.transform.GetChild(lastChild);
                plantExtensionPrefab = hitCollider.transform.GetChild(lastChild).gameObject;
                EnableSnakePlantPolymorph();
                var plantTypeScript = hitCollider.gameObject.GetComponent<PlantType>();
                player.transform.GetComponent<SnakeMovement>().SetCurrentPlant(
                    plantExtensionPrefab,
                    player,
                    plantTypeScript);

                oldPlantPosition = player.transform.position; //Care its used for the old player pos this time
                oldPlantParent = hitCollider.gameObject;
                oldPlantParent.transform.GetComponent<PlantType>().SetPlayerPlanted(true);
                player.transform.position = hitCollider.transform.GetChild(lastChild).position;
                currentPlant.SetParent(player.transform);
                polymorphSnakeActive = true;
                oldPlantParent.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                player.GetComponent<PhotonPlant>().SetPolyEnable(true);
                player.GetComponent<PhotonPlant>().SetWhichPlant(true);
                player.GetComponent<PhotonPlant>().SetPolyOwner();
                return;
            }
        }

        //Schaut ob Growable Polymorph aktiv ist und schaltet dies mithilfe von Methoden dann aus
        if (polymorphGrownActive)
        {
            ResetCCSize();
            DisableAllPolymorphs();
            var minY = player.transform.Find("Cylinder").GetComponent<Renderer>().bounds.min.y;
            currentPlant.SetParent(oldPlantParent.transform);
            currentPlant.position = oldPlantPosition;
            groundcheck.position = new Vector3(groundcheck.position.x, minY, groundcheck.position.z);
            cc.enabled = true;
            player.GetComponent<PhotonPlant>().SetPolyEnable(false);
        }

        //Schaut ob Snake Polymorph aktiv ist und schaltet dies mithilfe von Methoden dann aus
        if (polymorphSnakeActive)
        {
            DisableAllPolymorphs();
            currentPlant.SetParent(oldPlantParent.transform);
            player.transform.position = oldPlantPosition;
            player.transform.GetComponent<Player.PlayerMovement>().enabled = true;
            oldPlantParent.transform.GetComponent<PlantType>().SetPlayerPlanted(false);
            foreach (var plant in player.transform.GetComponent<SnakeMovement>().ReturnPlantList())
            {
                Destroy(plant);
            }

            player.GetComponent<PhotonPlant>().SetPolyEnable(false);
            player.transform.GetComponent<SnakeMovement>().ClearList();
            cc.enabled = true;
        }
    }

    private void EnableSnakePlantPolymorph()
    {
        cc = player.GetComponent<CharacterController>();
        player.GetComponent<Player.PlayerMovement>().enabled = false;
        player.GetComponent<SnakeMovement>().enabled = true;
        cc.enabled = false;
        player.transform.Find("Cylinder").gameObject.SetActive(false);
        player.transform.Find("Cube").gameObject.SetActive(false);
        player.transform.Find("Inventory").gameObject.SetActive(false);
        player.transform.Find("GroundCheck").gameObject.SetActive(false);
        polymorphSnakeActive = true;
    }

    private void EnableGrowablePlantPolymorph()
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

    private void ResetCCSize()
    {
        cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        cc.center = oldCharacterControllerCenter;
        cc.height = oldCharacterControllerHeight;
    }

    private void DisableAllPolymorphs()
    {
        player.transform.Find("Cylinder").gameObject.SetActive(true);
        player.transform.Find("Cube").gameObject.SetActive(true);
        player.transform.Find("Inventory").gameObject.SetActive(true);
        player.transform.Find("GroundCheck").gameObject.SetActive(true);
        player.transform.GetComponent<SnakeMovement>().enabled = false;
        polymorphGrownActive = false;
        polymorphSnakeActive = false;
    }
}
