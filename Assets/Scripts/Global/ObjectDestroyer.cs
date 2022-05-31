using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

namespace Global
{
    public class ObjectDestroyer : MonoBehaviourPun
    {
        public void DestroyTargetPun(GameObject gameObjectToDestroy)
        {
            var id = gameObjectToDestroy.GetPhotonView().ViewID;

            Debug.LogFormat("Sending 'Destroy GameObject' RPC-Call: Type={0}, ID={1}", gameObjectToDestroy, id);

            photonView.RPC("DestroyTarget", RpcTarget.MasterClient, id);
        }

        [PunRPC]
        [UsedImplicitly]
        private void DestroyTarget(int viewId)
        {
            var obj = PhotonView.Find(viewId).gameObject;

            Debug.LogFormat("[MASTER] Destroy GameObject: Type={0}, ID={1}", obj, viewId);

            PhotonNetwork.Destroy(obj);
        }
    }
}
