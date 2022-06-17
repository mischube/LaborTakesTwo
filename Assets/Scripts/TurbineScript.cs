using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineScript : MonoBehaviour
{
    [SerializeField]
    private Door.Door door;
    private bool openOnce;

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.name.Contains("Smoke") && !openOnce)
        {
            openOnce = true;
            door.Open();
        }
    }
}
