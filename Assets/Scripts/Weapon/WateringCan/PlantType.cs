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
    private Transform growingPlantTransform;
    private GameObject plantPrefab;
    public bool wateringParticle;
    

    private void Start()
    {
        plantPrefab = (GameObject) Resources.Load("Cannonball");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log(growingPlantTransform);
            stream.SendNext(growingPlantTransform);
            stream.SendNext(wateringParticle);
            Debug.Log(wateringParticle);
        } else if (stream.IsReading)
        {
            growingPlantTransform = (Transform) stream.ReceiveNext();
            Debug.Log(growingPlantTransform);
            SpawnPlantInMultiplayer();
            wateringParticle = (bool) stream.ReceiveNext();
            Debug.Log(wateringParticle);
        }
    }

    public void SetGrowingPlant(Transform transform)
    {
        growingPlantTransform = transform;
    }

    public void SpawnPlantInMultiplayer()
    {
        Debug.Log("Instantiate Prefab");
        //var var = Instantiate(plantPrefab, growingPlantTransform.position, growingPlantTransform.rotation);
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
    
}
