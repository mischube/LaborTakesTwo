using System;
using Photon.Pun;
using UnityEngine;

public class VictoryZone : MonoBehaviourPun
{
    public event PlayerEnteredZoneHandler PlayerEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerEvent?.Invoke(this);
        }
    }
}