using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParticel : MonoBehaviour
{
    public GameObject smokeParticel;
    
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(smokeParticel, transform.position, Quaternion.identity);
    }
}
