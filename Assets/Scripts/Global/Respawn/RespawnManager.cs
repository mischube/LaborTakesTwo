using System;
using Library.StringEnums;
using Photon.Pun;
using Player;
using UnityEngine;

namespace Global.Respawn
{
    public class RespawnManager : MonoBehaviour
    {
        private RespawnPointRepository _respawnPointRepository;
        private GameObject _playerPrefab;
        private GameObject _uiPrefab;


        private void Start()
        {
            _respawnPointRepository = gameObject.GetComponent<RespawnPointRepository>();
            _playerPrefab = Resources.Load<GameObject>("Player");
            _uiPrefab = Resources.Load<GameObject>("UI");
        }


        public void RespawnPlayer(GameObject player)
        {
            if (!player.CompareTag("Player"))
                throw new InvalidOperationException
                    ($"Cannot respawn a object which is not a player. Was: {player.tag}");

            if (GameManager.Instance.CurrentScene == Scenes.Start)
                throw new InvalidOperationException
                    ($"Can't spawn a player in Scene {GameManager.Instance.CurrentScene}");

            var checkpoint = player.GetComponent<AutoRespawn>().currentCheckpoint;

            TeleportPlayer(player, checkpoint.position);
        }


        private void TeleportPlayer(GameObject player, Vector3 position)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = position;
            player.GetComponent<CharacterController>().enabled = true;
        }


        public void SpawnPlayer()
        {
            var currentScene = GameManager.Instance.CurrentScene;
            var spawnPosition = _respawnPointRepository.GetSpawnPoint(currentScene);

            Debug.LogFormat
            ("Spawning player in scene {0} on position [{1}]", currentScene.GetStringValue(),
                spawnPosition.position);

            var player = PhotonNetwork.Instantiate(_playerPrefab.name, spawnPosition.position, Quaternion.identity);

            //Set default checkpoint in current scene
            player.GetComponent<AutoRespawn>().currentCheckpoint = spawnPosition;

            Instantiate(_uiPrefab);
        }
    }
}