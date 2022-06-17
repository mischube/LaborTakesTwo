using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Cannonball : MonoBehaviourPun
{
    [SerializeField]
    private float speed = 15f;
    
    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    public void DestoryWholeGameobject()
    {
        if (!photonView.IsMine)
            return;
        
        PhotonNetwork.Destroy(gameObject);
    }
}
