using System;
using System.Linq;
using Library.StringEnums;
using Photon.Pun;
using UnityEngine;

namespace Global.Respawn
{
    public class RespawnManager : MonoBehaviour
    {
        private RespawnPointRepository _respawnPointRepository;

        private void Start()
        {
            _respawnPointRepository = gameObject.GetComponent<RespawnPointRepository>();
        }

        public void RespawnPlayer(GameObject player)
        {
            var currentScene = GameManager.Instance.CurrentScene;

            if (!player.CompareTag("Player"))
                throw new InvalidOperationException
                    ($"Cannot respawn a object which is not a player. Was: {player.tag}");

            if (GameManager.Instance.CurrentScene == Scenes.Start)
                throw new InvalidOperationException
                    ($"Can't spawn a player in Scene {currentScene}");

            var spawnPoint = GetSpawnPoint(currentScene);


            TeleportPlayer(player, spawnPoint);
            //todo set player HP
        }

        private void TeleportPlayer(GameObject player, RespawnPoint activePoint)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = activePoint.position;
            player.GetComponent<CharacterController>().enabled = true;
        }

        public void SpawnPlayer()
        {
            var currentScene = GameManager.Instance.CurrentScene;
            var spawnPosition = GetSpawnPoint(currentScene);

            var playerPrefab = Resources.Load<GameObject>("Player");
            var uiPrefab = Resources.Load<GameObject>("UI");

            Debug.LogFormat
            ("Spawning player in scene {0} on position [{1}]", currentScene.GetStringValue(),
                spawnPosition.position);

            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity);

            Instantiate(uiPrefab);
        }

        private RespawnPoint GetSpawnPoint(Scenes scene)
        {
            var respawnPoints = _respawnPointRepository.GetRespawnPoints(scene);
            var activePoint = respawnPoints.SingleOrDefault(point => point.isChecked);
            return activePoint;
        }
    }
}