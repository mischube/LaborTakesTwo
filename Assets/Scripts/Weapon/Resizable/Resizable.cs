using Photon.Pun;
using UnityEngine;

namespace Weapon.Resizable
{
    public abstract class Resizable : MonoBehaviourPun
    {
        [SerializeField] protected float resizeFactor;

        public float ResizeFactor => resizeFactor;

        public abstract void Resize();

        protected void RPCCall(string methodName)
        {
            photonView.RPC(methodName, RpcTarget.All);
        }
    }
}
