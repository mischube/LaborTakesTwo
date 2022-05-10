using System;
using Player;
using UnityEngine;

namespace Global.Respawn
{
    public class RespawnManager : MonoBehaviour
    {
        private void Start()
        {
            PlayerNetworking.PlayerLoaded += () =>
            {
                PlayerNetworking.LocalPlayerInstance.GetComponent<PlayerHealth>().PlayerDeadEvent += OnPlayerDead;
            };
        }


        public void RespawnPlayer(GameObject player)
        {
            if (!player.CompareTag("Player"))
                throw new InvalidOperationException
                    ($"Cannot respawn a object which is not a player. Was: {player.tag}");


            var checkpoint = player.GetComponent<Player.Respawn>().currentCheckpoint;

            Debug.LogFormat("Respawning player at {0}", checkpoint.position);
            
            TeleportPlayer(player, checkpoint.position);
        }


        private void TeleportPlayer(GameObject player, Vector3 position)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = position;
            player.GetComponent<CharacterController>().enabled = true;
        }


        private void OnPlayerDead()
        {
            RespawnPlayer(PlayerNetworking.LocalPlayerInstance);
        }
    }
}