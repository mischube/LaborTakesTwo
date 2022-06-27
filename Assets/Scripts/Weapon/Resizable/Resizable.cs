using Photon.Pun;
using Player;
using UnityEngine;

namespace Weapon.Resizable
{
    public abstract class Resizable : MonoBehaviourPun
    {
        [SerializeField] protected float resizeFactor;
        [SerializeField] protected bool isPlayer;


        public abstract void Resize();

        protected void RPCCall(string methodName)
        {
            photonView.RPC(methodName, RpcTarget.All);
        }

        protected void UpdatePlayerStats()
        {
            Debug.LogFormat("resizing player stats: obj={0}, factor= {1}", gameObject, resizeFactor);

            var playerMovement = GetComponent<Player.PlayerMovement>();
            playerMovement.characterController.stepOffset *= resizeFactor;

            playerMovement.speed *= resizeFactor;
            playerMovement.dashSpeed *= resizeFactor;
            playerMovement.jumpHeight *= resizeFactor;
        }
    }
}
