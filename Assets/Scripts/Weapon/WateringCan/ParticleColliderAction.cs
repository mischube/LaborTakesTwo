using System.Collections.Generic;
using UnityEngine;

public class ParticleColliderAction : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem particleSystem;
    private float growthSpeed = 0.011f;
    private float maxSize = 1f;
    private string plantTypeTag;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem = transform.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Plant"))
        {
            maxSize = other.GetComponent<PlantType>().getPlantSize();
            plantTypeTag = other.GetComponent<PlantType>().getPlantType();
            particleSystem.GetCollisionEvents(other, collisionEvents);
            if (plantTypeTag.Equals("Growable") && other.transform.GetChild(0).localScale.y < maxSize)
            {
                other.transform.GetChild(0).position += new Vector3(0, growthSpeed * 5, 0);
                other.transform.GetChild(0).localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed);
            } else
            {
                if (plantTypeTag.Equals("Growable"))
                    other.tag = "Grown";
            }
        }
    }
}
