using System;
using Networking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        public Scenes defaultScene = Scenes.Prototype;

        private NetworkManager _networkManager;
        private Guid _guid;

        public static GameManager Instance => GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        private void Start()
        {
            _guid = Guid.NewGuid();
            Debug.Log($"Starting game manager [{_guid}]");

            LoadComponents();

            SceneManager.sceneLoaded += OnSceneLoaded;
            _networkManager.OnLobbyJoined += OnLobbyJoined;

            _networkManager.Connect();

            Debug.Log("game manager started");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            Debug.LogFormat("Scene '{0}' loaded", scene.name);
            _networkManager.SpawnPlayer(new Vector3(65, 16, -43));
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (_networkManager != null)
            {
                _networkManager.OnLobbyJoined -= OnLobbyJoined;
            }
        }

        private void OnLobbyJoined(object sender)
        {
            Debug.LogFormat("Player joined room");

            if (PhotonNetwork.IsMasterClient)
            {
                //Only first player joining room loads a scene
                _networkManager.LoadScene(defaultScene);
            } else
            {
                //all others just spawn their player object
                _networkManager.SpawnPlayer(new Vector3(65, 16, -43));
            }
        }

        private void LoadComponents()
        {
            _networkManager = gameObject.GetComponent<NetworkManager>();
        }

        public void SwitchScene()
        {
            _networkManager.LoadScene(Scenes.Advanced);
            //todo spawn players at correct point
        }
    }
}