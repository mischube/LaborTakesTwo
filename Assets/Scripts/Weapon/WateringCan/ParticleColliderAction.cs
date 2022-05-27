using System.Collections.Generic;
using UnityEngine;

public class ParticleColliderAction : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem particleSystem;
    private float growthSpeed = 0.011f;
    private float maxSize = 1f;
    private int maxSnakeSize = 4;
    private int currentSnakeSize = 0;
    private string plantTypeTag;
    private GameObject plantpart;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem = transform.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Plant"))
        {
            maxSize = other.GetComponent<PlantType>().GetPlantSize();
            maxSnakeSize = other.GetComponent<PlantType>().GetSnakePlantSize();
            currentSnakeSize = other.GetComponent<PlantType>().GetCurrentSnakeSize();
            plantTypeTag = other.GetComponent<PlantType>().GetPlantType();
            particleSystem.GetCollisionEvents(other, collisionEvents);

            if (other.CompareTag("Plant") && plantTypeTag.Equals("Growable") &&
                other.transform.GetChild(0).localScale.y < maxSize)
            {
                other.transform.GetChild(0).position += new Vector3(0, growthSpeed * 5, 0);
                other.transform.GetChild(0).localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed);
            } else
            {
                if (plantTypeTag.Equals("Growable"))
                    other.tag = "Grown";
            }

            if (other.CompareTag("Plant") && plantTypeTag.Equals("Snake") &&
                other.transform.childCount < maxSnakeSize / 10)
            {
                other.GetComponent<PlantType>().IncrementCurrentSnakeSize();
                if (currentSnakeSize % 10 == 0 && currentSnakeSize > 0)
                {
                    int childCount = currentSnakeSize / 10 - 1;
                    plantpart = other.transform.GetChild(childCount).gameObject;
                    Vector3 nextPos = plantpart.transform.position;
                    nextPos.y = plantpart.transform.position.y + 1;

                    Instantiate(
                        plantpart,
                        nextPos,
                        plantpart.transform.rotation,
                        other.transform);
                }
            } else
            {
                if (plantTypeTag.Equals("Snake"))
                    other.tag = "GrownSnake";
            }
        }
    }
}
