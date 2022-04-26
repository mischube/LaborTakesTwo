using Library.StringEnums;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LocalScenesManager : MonoBehaviourPun
    {
        public GameObject playerPrefab;

        private Scenes _currentScene;

        private void Start()
        {
            playerPrefab = Resources.Load<GameObject>("Player");
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
            PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            Debug.LogFormat("Scene '{0}' loaded", scene.name);
        }
    }
}