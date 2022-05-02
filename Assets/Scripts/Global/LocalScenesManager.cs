using System;
using Library.StringEnums;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LocalScenesManager : MonoBehaviourPun
    {
        private Scenes _currentScene;

        public Scenes CurrentScene
        {
            get
            {
                var scene = SceneManager.GetActiveScene().name;
                var success = Enum.TryParse<Scenes>(scene, out var scenesEnumValue);
                return success ? scenesEnumValue : throw new InvalidCastException();
            }
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void LoadScene(Scenes levelName)
        {
            Debug.LogFormat("Loading scene [{0}]", levelName.GetStringValue());

            PhotonNetwork.LoadLevel(levelName.GetStringValue());
            _currentScene = levelName;
        }


        public void SpawnPlayer(Vector3 position)
        {
            Debug.LogFormat
            ("Spawning player in scene {0} on position [{1}]", SceneManager.GetActiveScene().name,
                position);

            var playerPrefab = Resources.Load<GameObject>("Player");
            var uiPrefab = Resources.Load<GameObject>("UI");

            PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);

            Instantiate(uiPrefab);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            Debug.LogFormat("Scene '{0}' loaded", scene.name);
        }
    }
}