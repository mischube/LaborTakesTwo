using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MeteorDmg : MonoBehaviour
{
    private float meteorDmg = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer(meteorDmg);
        }
    }
}
