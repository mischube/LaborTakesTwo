using System;
using Photon.Pun;
using UnityEngine;

public class Meteor : MonoBehaviourPun
{
    public Transform impact;
    public Transform meteor;

    private void Update()
    {
        if (!photonView.IsMine)
            return;
        
        if (Math.Abs(impact.position.y - meteor.position.y) < .1f) //checks if impact and meteor are on same y level
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
