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
                hammer.GetAttackActive())
            {
                other.GetComponent<Destroyable>().Destroy();
            }

            if (other.gameObject.CompareTag("Pushable") &&
                hammer.GetPushActive())
            {
                other.GetComponent<Movable>().MoveTargetPun(transform.forward);
            }
        }
    }
}