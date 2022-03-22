using System;
using Photon.Pun;
using UnityEngine;

public class PlattformMovement : MonoBehaviourPun
{
    public float speed = 1f;


    // Update is called once per frame
    void Update()
    {
        return;
        if (!photonView.IsMine)
            return;

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (!(direction.magnitude >= 0.1f))
            return;

        transform.position += direction * speed * Time.deltaTime;
    }

    public void MoveInDirection(Vector3 direction)
    {
        if (!photonView.IsMine)
            return;
        
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}