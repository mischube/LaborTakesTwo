using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{

    private float speed = .05f;
    
    private void Update()
    {
        transform.position += transform.forward * speed;
    }
}
