using System;
using Photon.Pun;
using UnityEngine;

public class CannonballDmg : MonoBehaviour
{
    private Cannonball cannonball;
    
    private void Start()
    {
        cannonball = GetComponentInParent<Cannonball>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            bossHealth.DamageBoss(3f);
            cannonball.DestoryWholeGameobject();
        }
    }
}
