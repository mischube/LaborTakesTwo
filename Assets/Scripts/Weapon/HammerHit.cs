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
    }
}
