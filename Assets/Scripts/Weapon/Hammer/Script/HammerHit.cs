using Photon.Pun;
using UnityEngine;
using Weapon;

public class HammerHit : MonoBehaviourPun
{
    public Hammer hammer;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Destroyable") &&
            hammer.getAttackActive())
        {
            collider.GetComponent<Moveable>().DestroyTargetPun();
        }

        if (collider.gameObject.CompareTag("Pushable") &&
            hammer.getPushActive())
        {
            collider.GetComponent<Moveable>().MoveTargetPun(transform.forward);
        }
    }
}