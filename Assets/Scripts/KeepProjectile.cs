using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class KeepProjectile : MonoBehaviour
{
    private bool activatedOnce;
    [SerializeField] private String[] possibleTags;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private String partNameOfProjectile;
    
    private void OnTriggerEnter(Collider other)
    {
        if (activatedOnce)
            return;
            
        foreach (var wantedTag in possibleTags)
        {
            if (other.transform.tag.Equals(wantedTag) && other.name.Contains(partNameOfProjectile))
            {
                activatedOnce = true;
                GameObject projectile = Instantiate(ProjectilePrefab,
                    transform.position, transform.rotation);
                projectile.GetComponent<MoveProjectileAndDestroy>().enabled = false;
                projectile.transform.position = new Vector3(projectile.transform.position.x,projectile.transform.position.y-1f,projectile.transform.position.z);
            }
        }
    }
}
