using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlattformMovement : MonoBehaviourPun
{
    public float speed = 1f;

    private Vector3 _difference;


    public void MoveInDirection(Vector3 direction)
    {
        if (!photonView.IsMine)
            return;

        _difference = direction.normalized * speed * Time.deltaTime;

        transform.position += _difference;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().Move(_difference);
        }
    }
}