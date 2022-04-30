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
        if (collider.gameObject.CompareTag("Pushable") && hammer.getPushActive())
        {
            Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward*20,ForceMode.Impulse);
            StartCoroutine(FreezeObject(rigidbody));
        }
    }
    IEnumerator FreezeObject(Rigidbody rigidbody)
    {
        yield return new WaitForSeconds(3);
        rigidbody.isKinematic = true;
    }
}
