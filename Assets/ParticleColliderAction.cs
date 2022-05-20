using System.Collections.Generic;
using UnityEngine;

public class ParticleColliderAction : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem particleSystem;
    private float growthSpeed = 0.1f;
    private float maxSize = 8f;
    [SerializeField] private GameObject grownPlantPrefab;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem = transform.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        if (other.CompareTag("Growable") && other.transform.GetChild(0).localScale.y < maxSize)
        {
            other.transform.GetChild(0).position += new Vector3(0, growthSpeed / 5, 0);
            other.transform.GetChild(0).localScale += new Vector3(0, growthSpeed, 0);
        }

        if (other.CompareTag("Growable") && other.transform.GetChild(0).localScale.y >= maxSize)
        {
            Destroy(other.transform.GetChild(0).gameObject);
            GameObject gameObject = Instantiate(
                grownPlantPrefab,
                other.transform.GetChild(0).position,
                other.transform.GetChild(0).rotation);
            gameObject.gameObject.transform.SetParent(other.gameObject.transform);
            gameObject.tag = "Grown";
            other.tag = "Grown";
        }
    }
}
