using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MoveProjectileAndDestroy : MonoBehaviourPun
{
    [SerializeField]
    private float speed = 15f;
    
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterTravel());
    }

    IEnumerator DestroyAfterTravel()
    {
        yield return new WaitForSeconds(3f);
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);   
        }
    }
}
