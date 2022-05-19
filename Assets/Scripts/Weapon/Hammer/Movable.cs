using System.Collections;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Hammer
{
    public class Movable : MonoBehaviourPun
    {
        public void DestroyTargetPun()
        {
            photonView.RPC("DestroyTarget", RpcTarget.MasterClient);
        }

        public void MoveTargetPun(Vector3 forwardDirection)
        {
            photonView.RPC("MoveTarget", RpcTarget.All, forwardDirection);
        }

        [PunRPC]
        [UsedImplicitly]
        private void DestroyTarget()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        [UsedImplicitly]
        private void MoveTarget(Vector3 forward)
        {
            var rigidbodyComponent = transform.gameObject.GetComponent<Rigidbody>();
            rigidbodyComponent.isKinematic = false;
            rigidbodyComponent.AddForce(forward * 10, ForceMode.Impulse);
            StartCoroutine(FreezeObject(rigidbodyComponent));
        }

        IEnumerator FreezeObject(Rigidbody rigidBody)
        {
            yield return new WaitForSeconds(3);
            rigidBody.isKinematic = true;
        }
    }
}