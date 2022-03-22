using System;
using Photon.Pun;
using UnityEngine;

public class Arrow : MonoBehaviourPun
{
    public PlattformMovement plattformMovement;

    public Vector3 direction;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            plattformMovement.MoveInDirection(direction);
        }
    }
}