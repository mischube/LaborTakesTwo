using UnityEngine;
using Weapon.WateringCan;

public class PlantType : MonoBehaviour
{
    [Tooltip("Possible Types 'Snake', 'Growable'")] [SerializeField]
    private Plants plantType;

    [Tooltip("Gives the plant a max size, works for all plants")] [SerializeField]
    private float plantGrowthSize;

    public string getPlantType()
    {
        return plantType.ToString();
    }

    public float getPlantSize()
    {
        return plantGrowthSize;
    }
}
