using Photon.Pun;
using UnityEngine;

namespace Weapon.Hammer
{
    public class HammerHit : MonoBehaviourPun
    {
        public Hammer hammer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Destroyable") &&
                hammer.getAttackActive())
            {
                other.GetComponent<Moveable>().DestroyTargetPun();
            }

            if (other.gameObject.CompareTag("Pushable") &&
                hammer.getPushActive())
            {
                other.GetComponent<Moveable>().MoveTargetPun(transform.forward);
            }
        }
    }
}