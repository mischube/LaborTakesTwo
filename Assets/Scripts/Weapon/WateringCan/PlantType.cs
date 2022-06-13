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

    private int currentPlantGrowthSize = 0;

    private int maxSnakeRange;
    private Vector3 growingPlantTransform;
    private GameObject plantPrefab;
    private GameObject oldPlayerPrefab;
    private List<GameObject> plants;
    private bool playerIsPlanted = false;
    private bool playerIsPoly = false;

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
            stream.SendNext(growingPlantTransform);
         
        } else if (stream.IsReading)
        {
            playerIsPlanted = (bool) stream.ReceiveNext();
            growingPlantTransform = (Vector3) stream.ReceiveNext();
            SpawnPlantInMultiplayer();
            DeleteAllPlants();
        }
    }

    public void SetGrowingPlant(Vector3 vector3)
    {
        growingPlantTransform = vector3;
    }

    public void PlayerIsPoly(bool poly)
    {
        playerIsPoly = poly;
    }

    public void SpawnPlantInMultiplayer()
    {
        if (plantPrefab == null)
            return;
        if (playerIsPlanted)
        {
            PlayerIsPoly(true);
            var var = Instantiate(plantPrefab, growingPlantTransform, Quaternion.identity);
            plants.Add(var);
        }
    }

    public void DeleteAllPlants()
    {
        if (!playerIsPlanted)
        {
            PlayerIsPoly(false);
            if (plants.Count == 0)
                return;
            foreach (var plant in plants)
            {
                Destroy(plant);
            }

            plants.Clear();
            ResetPlantPos();
        }
    }

    #region Getters

    public string GetPlantType()
    {
        return plantType.ToString();
    }

    public int GetSnakeRange()
    {
        return maxSnakeRange;
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
        ResetPlantPos();
        playerIsPlanted = status;
    }

    public void ResetPlantPos()
    {
        growingPlantTransform = new Vector3(-30, -30, -30);
    }
}
