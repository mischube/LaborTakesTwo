using System;
using Photon.Pun;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Transform impact;
    public Transform meteor;

    private void Update()
    {
        if (Math.Abs(impact.position.y - meteor.position.y) < .1f) //checks if impact and meteor are on same y level
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
