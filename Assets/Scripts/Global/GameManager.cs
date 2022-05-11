using System;
using Global.Respawn;
using Library.StringEnums;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
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
            _respawnManager.RespawnPlayer(PlayerNetworking.LocalPlayerInstance);
        }

        #endregion


        public void SwitchScene(Scenes nextScene)
        {
            _scenesManager.LoadScene(nextScene);

            //Set spawn point in new scene
            var spawnPoint = _spawnPointRepository.GetSpawnPoint(nextScene);
            PlayerNetworking.LocalPlayerInstance.GetComponent<Player.Respawn>().currentCheckpoint = spawnPoint;
        }
    }
}
