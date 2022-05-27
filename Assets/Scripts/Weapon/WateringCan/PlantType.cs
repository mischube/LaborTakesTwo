using UnityEngine;
using Weapon.WateringCan;

public class PlantType : MonoBehaviour
{
    [Tooltip("Possible Types 'Snake', 'Growable'")] [SerializeField]
    private Plants plantType;

    [Tooltip("Gives the plant a max size, works for all plants")] [SerializeField]
    private float plantGrowthSize;

    [Tooltip("Gives the snake plant a max size")] [SerializeField]
    private int snakePlantGrowthSize;

    private int currentPlantGrowthSize = 0;

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

    public void IncrementCurrentSnakeSize()
    {
        currentPlantGrowthSize++;
    }
}
