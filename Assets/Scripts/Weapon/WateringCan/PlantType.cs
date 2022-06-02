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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(growingPlantTransform);
        } else if (stream.IsReading)
        {
            growingPlantTransform = (Transform) stream.ReceiveNext();
            SpawnPlantInMultiplayer();
        }
    }

    public void SetGrowingPlant(Transform transform, GameObject gameObject)
    {
        growingPlantTransform = transform;
        plantPrefab = gameObject;
    }

    public void SpawnPlantInMultiplayer()
    {
        if (photonView.IsMine)
            return;
        Instantiate(plantPrefab);
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
