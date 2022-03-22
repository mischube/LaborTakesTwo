using System;
using Photon.Pun;
using UnityEngine;

public class PlattformMovement : MonoBehaviourPun
{
    public float speed = 1f;


    public void MoveInDirection(Vector3 direction)
    {
        if (!photonView.IsMine)
            return;

        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}