using System;
using Global.Respawn;
using Library.StringEnums;
using Photon.Pun;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviourPun
    {
        private LocalScenesManager _scenesManager;
        private RespawnManager _respawnManager;
        private SpawnPointRepository _spawnPointRepository;

        private readonly Guid _guid = Guid.NewGuid();

        public static GameManager Instance => GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


        public Scenes CurrentScene
        {
            get
            {
                var scene = SceneManager.GetActiveScene();
                return scene.GetEnumValue();
            }
        }


        private void Start()
        {
            Debug.Log($"Starting game manager [{_guid}]");

            LoadComponents();
            SubscribeEvents();

            Debug.Log("game manager started");
        }


        private void LoadComponents()
        {
            _scenesManager = GetComponent<LocalScenesManager>();
            _respawnManager = GetComponent<RespawnManager>();
            _spawnPointRepository = GetComponent<SpawnPointRepository>();
        }


        private void SubscribeEvents()
        {
            PlayerNetworking.PlayerLoaded += () =>
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            };
        }


        #region Events

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log($"Scene {CurrentScene.GetStringValue()} loaded");

            SetSpawnPoint();

            _respawnManager.RespawnPlayer(PlayerNetworking.LocalPlayerInstance);
        }

        #endregion


        public void SwitchScene(Scenes nextScene)
        {
            Debug.Log($"[GameManager] Switching to Scene {nextScene.GetStringValue()}");
            _scenesManager.LoadScene(nextScene);
        }

        private void SetSpawnPoint()
        {
            var spawnPoint = _spawnPointRepository.GetSpawnPoint(CurrentScene);

            Debug.Log($"Switching player respawn: {spawnPoint.position}");

            PlayerNetworking.LocalPlayerInstance.GetComponent<Player.Respawn>().currentCheckpoint = spawnPoint;
        }
    }
}
