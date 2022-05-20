using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class IceProjectile : MonoBehaviour
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
        PhotonNetwork.Destroy(gameObject);
    }
}
