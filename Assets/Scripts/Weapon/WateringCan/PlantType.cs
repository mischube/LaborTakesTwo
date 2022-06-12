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
    private bool playerIsPlanted = false;
    public int counter = 0;


    private void Start()
    {
        plantPrefab = (GameObject) Resources.Load("Plantparts");
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
        }
    }

    public void SetGrowingPlant(Vector3 vector3)
    {
        growingPlantTransform = vector3;
    }

    public void SpawnPlantInMultiplayer()
    {
        Debug.Log("Instantiate Prefab");
        if (plantPrefab == null)
            return;
        if (playerIsPlanted)
        {
            var var = Instantiate(plantPrefab, growingPlantTransform, Quaternion.identity);
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

    public void setplayerplanted(bool status)
    {
        playerIsPlanted = status;
    }
}
