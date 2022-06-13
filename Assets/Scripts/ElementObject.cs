using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementObject : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.cyan;
        transform.tag = "Destroyable";
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Contains("Fire"))
        {
            FireHit();
        } 
        else if (other.name.Contains("Ice"))
        {
            IceHit();
        }
    }

    private void FireHit()
    {
        if (transform.gameObject.layer == 12)
        {
            transform.gameObject.layer = 4;
            GetComponent<Renderer>().material.color = Color.blue;
            transform.tag = "Untagged";
        }else if (transform.gameObject.layer == 13)
        {
            Destroy(gameObject);
        }
    }
    
    private void IceHit()
    {
        if (transform.gameObject.layer == 4)
        {
            transform.gameObject.layer = 12;
            GetComponent<Renderer>().material.color = Color.cyan;
            transform.tag = "Destroyable";
        }
    }
}
