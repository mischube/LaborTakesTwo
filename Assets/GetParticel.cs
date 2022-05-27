using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParticel : MonoBehaviour
{
    public GameObject smokeParticel;
    public string rodName;
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Contains("Ice") && rodName.Equals("Fire"))
        {
            Instantiate(smokeParticel, transform.position, smokeParticel.transform.rotation);
        }
        else if (other.name.Contains("Fire") && rodName.Equals("Ice"))
        {
            Instantiate(smokeParticel, transform.position, smokeParticel.transform.rotation);
        }
    }
}
