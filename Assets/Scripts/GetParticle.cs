using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParticle : MonoBehaviour
{
    public GameObject smokeParticle;
    public string rodName;
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Contains("Ice") && rodName.Equals("Fire"))
        {
            Instantiate(smokeParticle, transform.position, smokeParticle.transform.rotation);
        }
        else if (other.name.Contains("Fire") && rodName.Equals("Ice"))
        {
            Instantiate(smokeParticle, transform.position, smokeParticle.transform.rotation);
        }
    }
}
