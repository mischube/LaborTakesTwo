using System.Collections.Generic;
using UnityEngine;

public class ParticleColliderAction : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem particleSystem;
    private float growthSpeed = 0.011f;
    private float maxSize = 1f;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem = transform.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        particleSystem.GetCollisionEvents(other, collisionEvents);
        if (other.CompareTag("Growable") && other.transform.GetChild(0).localScale.y < maxSize)
        {
            other.transform.GetChild(0).position += new Vector3(0, growthSpeed * 5, 0);
            other.transform.GetChild(0).localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed);
        } else
        {
            if (other.CompareTag("Growable"))
                other.tag = "Grown";
        }
    }
}
