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

        private LocalScenesManager _scenesManager;
        private NetworkManager _networkManager;

        private readonly Guid _guid = Guid.NewGuid();

        public static GameManager Instance => GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        private void Start()
        {
            Debug.Log($"Starting game manager [{_guid}]");

            LoadComponents();

            _networkManager.OnLobbyJoined += OnLobbyJoined;
            _networkManager.Connect();

            Debug.Log("game manager started");
        }


        private void OnDisable()
        {
            if (_networkManager != null)
                _networkManager.OnLobbyJoined -= OnLobbyJoined;
        }

        private void OnLobbyJoined(object sender)
        {
            if (PhotonNetwork.IsMasterClient && _scenesManager.CurrentScene == Scenes.Start)
            {
                //Only first player joining room loads a scene
                _scenesManager.LoadScene(defaultScene);
            }

            //all others just spawn their player object
            _scenesManager.SpawnPlayer(new Vector3(65, 16, -43));
        }

        private void LoadComponents()
        {
            _scenesManager = gameObject.GetComponent<LocalScenesManager>();
            _networkManager = gameObject.GetComponent<NetworkManager>();
        }

        public void SwitchScene(Scenes nextScene)
        {
            _scenesManager.LoadScene(nextScene);

            //todo rework this part when the respawnSystem is implemented
            Player.PlayerNetworking.LocalPlayerInstance.GetComponent<CharacterController>().enabled = false;
            Player.PlayerNetworking.LocalPlayerInstance.transform.position = new Vector3(65, 16, -43);
            Player.PlayerNetworking.LocalPlayerInstance.GetComponent<CharacterController>().enabled = true;
        }
    }
}