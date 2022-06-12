using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class KeepProjectile : MonoBehaviour
{
    private bool activatedOnce;
    [SerializeField] private String[] possibleTags;
    
    private void OnTriggerEnter(Collider other)
    {
        if (activatedOnce)
            return;
            
        foreach (var wantedTag in possibleTags)
        {
            if (other.transform.tag.Equals(wantedTag))
            {
                activatedOnce = true;
                GameObject projectile = PhotonNetwork.Instantiate(other.name.Substring(0, other.name.Length - 7),
                    new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation);
                projectile.GetComponent<MoveProjectileAndDestroy>().enabled = false;
            }
        }
    }
}
