using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActivation : MonoBehaviour
{
    public Cannon cannon;
    
    private void OnTriggerEnter(Collider other)
    {
        cannon.ShootCannonBall();
    }
}
