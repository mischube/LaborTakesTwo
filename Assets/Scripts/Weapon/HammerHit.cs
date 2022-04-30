using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class HammerHit : MonoBehaviour
{
    public Hammer hammer;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Destroyable") && hammer.getAttackActive())
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("Pushable"))
        {
            Debug.Log("I hit shit");
            collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.up*100,ForceMode.Force);
        }
    }
}
