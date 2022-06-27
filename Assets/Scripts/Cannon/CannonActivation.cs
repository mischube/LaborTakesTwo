using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActivation : MonoBehaviour
{
    public Cannon cannon;
    private bool shootOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (!shootOnce)
        {
            shootOnce = true;
            cannon.ShootCannonBall();
        }
    }
}
