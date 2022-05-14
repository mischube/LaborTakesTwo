using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Hammer
{
    public class Moveable : MonoBehaviourPun
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
        private void DestroyTarget()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        private void MoveTarget(Vector3 forward)
        {
            Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(forward * 10, ForceMode.Impulse);
            StartCoroutine(FreezeObject(rigidbody));
        }

        IEnumerator FreezeObject(Rigidbody rigidbody)
        {
            yield return new WaitForSeconds(3);
            rigidbody.isKinematic = true;
        }
    }
}