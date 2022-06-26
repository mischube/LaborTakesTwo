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
            var player = PlayerNetworking.LocalPlayerInstance;

            var playerMovement = player.GetComponent<Player.PlayerMovement>();
            playerMovement.characterController.stepOffset *= resizeFactor;

            playerMovement.speed *= resizeFactor;
            playerMovement.dashSpeed *= resizeFactor;
            playerMovement.jumpHeight *= resizeFactor;
        }
    }
}
