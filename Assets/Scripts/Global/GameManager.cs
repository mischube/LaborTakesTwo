using System;
using Global.Respawn;
using Networking;
using Photon.Pun;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        public Scenes defaultScene = Scenes.Prototype;

        private LocalScenesManager _scenesManager;
        private NetworkManager _networkManager;
        private RespawnManager _respawnManager;

        private readonly Guid _guid = Guid.NewGuid();

        public static GameManager Instance => GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


        public Scenes CurrentScene => _scenesManager.CurrentScene;


        private void Start()
        {
            Debug.Log($"Starting game manager [{_guid}]");

            LoadComponents();

            _networkManager.OnLobbyJoined += OnLobbyJoined;
            _networkManager.Connect();

            PlayerNetworking.PlayerLoaded += () =>
            {
                PlayerNetworking.LocalPlayerInstance.GetComponent<PlayerHealth>().playerDeadEvent += OnPlayerDeadEvent;
            };

            Debug.Log("game manager started");
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("Scene loaded");
            _respawnManager.RespawnPlayer(PlayerNetworking.LocalPlayerInstance);
        }


        private void OnPlayerDeadEvent()
        {
            _respawnManager.RespawnPlayer(PlayerNetworking.LocalPlayerInstance);
        }


        private void OnDisable()
        {
            if (_networkManager != null)
                _networkManager.OnLobbyJoined -= OnLobbyJoined;
        }


        private void OnLobbyJoined(object sender)
        {
            if (PhotonNetwork.IsMasterClient &&
                _scenesManager.CurrentScene == Scenes.Start)
            {
                //Only first player joining room loads a scene
                _scenesManager.LoadScene(defaultScene);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;

            //all others just spawn their player object
            _respawnManager.SpawnPlayer();
        }


        private void LoadComponents()
        {
            _scenesManager = gameObject.GetComponent<LocalScenesManager>();
            _networkManager = gameObject.GetComponent<NetworkManager>();
            _respawnManager = gameObject.GetComponent<RespawnManager>();
        }


        public void SwitchScene(Scenes nextScene)
        {
            _scenesManager.LoadScene(nextScene);
        }
    }
}