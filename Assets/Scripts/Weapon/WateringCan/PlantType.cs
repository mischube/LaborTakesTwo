using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Weapon.WateringCan;

public class PlantType : MonoBehaviourPun, IPunObservable
{
    [Tooltip("Possible Types 'Snake', 'Growable'")] [SerializeField]
    private Plants plantType;

    [Tooltip("Gives the plant a max size, works for all plants")] [SerializeField]
    private float plantGrowthSize;

    [Tooltip("Gives the snake plant a max size")] [SerializeField]
    private int snakePlantGrowthSize;

    private int currentPlantGrowthSize;
    [SerializeField] private int maxSnakeRange;
    private Vector3 growingPlantTransform;
    private GameObject plantPrefab;
    private GameObject oldPlayerPrefab;
    private List<GameObject> plants;
    private bool playerIsPlanted;
    private bool playerIsPlanting;
    private bool ownerOfPlant;

    private void Start()
    {
        plantPrefab = (GameObject) Resources.Load("Plantparts");
        plants = new List<GameObject>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerIsPlanted);
            stream.SendNext(playerIsPlanting);
            stream.SendNext(growingPlantTransform);
        } else if (stream.IsReading)
        {
            playerIsPlanted = (bool) stream.ReceiveNext();
            playerIsPlanting = (bool) stream.ReceiveNext();
            growingPlantTransform = (Vector3) stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (playerIsPlanting && !ownerOfPlant)
            SpawnPlantInMultiplayer();
        if (plants.Count == 0)
            return;
        if (!playerIsPlanted)
        {
            DeleteAllPlants();
        }
    }

    public void SetGrowingPlant(Vector3 vector3)
    {
        growingPlantTransform = vector3;
    }

    public void SetPolyOwner()
    {
        ownerOfPlant = true;
    }

    public void PlayerIsPlanting(bool poly)
    {
        playerIsPlanting = poly;
    }

    public int GetMaxSnakeRange()
    {
        return maxSnakeRange;
    }

    public void SpawnPlantInMultiplayer()
    {
        if (plantPrefab == null)
            return;
        if (playerIsPlanted)
        {
            var var = Instantiate(plantPrefab, growingPlantTransform, Quaternion.identity);
            plants.Add(var);
            playerIsPlanting = false;
        }
    }

    public void DeleteAllPlants()
    {
        foreach (var plant in plants)
        {
            Destroy(plant);
        }

        plants.Clear();
    }

    #region Getters

    public string GetPlantType()
    {
        return plantType.ToString();
    }

    public float GetPlantSize()
    {
        return plantGrowthSize;
    }

    public int GetSnakePlantSize()
    {
        return snakePlantGrowthSize;
    }

    public int GetCurrentSnakeSize()
    {
        return currentPlantGrowthSize;
    }

    #endregion

    public void IncrementCurrentSnakeSize()
    {
        currentPlantGrowthSize++;
    }

    public void SetPlayerPlanted(bool status)
    {
        playerIsPlanted = status;
    }
}
