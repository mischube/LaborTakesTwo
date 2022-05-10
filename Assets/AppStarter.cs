using Global;
using Global.Respawn;
using Library.StringEnums;
using Photon.Pun;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStarter : MonoBehaviour
{
    [SerializeField] private Scenes defaultScene;
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private GameObject playerPrefab;

    private NetworkConnector _networkConnector;

    private void Start()
    {
        SetDefaultScene();

        SceneManager.sceneLoaded += OnGameSceneLoaded;
        _networkConnector = GetComponent<NetworkConnector>();
        _networkConnector.JoinedRoom += OnJoinedRoom;
        _networkConnector.Connect();
        _networkConnector.Connected += () => _networkConnector.JoinRoom();

        Instantiate(gameManagerPrefab);
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.GetEnumValue() == Scenes.Start)
            return;

        SpawnPlayer();
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }


    private void OnJoinedRoom()
    {
        LoadGameScene();
    }


    private void LoadGameScene()
    {
        Debug.LogFormat("load game scene {0}", defaultScene.GetStringValue());

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(defaultScene.GetStringValue());
        }


        _networkConnector.JoinedRoom -= OnJoinedRoom;
    }

    private void SetDefaultScene()
    {
        if (AppStartup.Scene == Scenes.Start)
        {
            return;
        }

        defaultScene = AppStartup.Scene;
    }


    private void SpawnPlayer()
    {
        var gameManager = GameManager.Instance;
        var currentScene = gameManager.CurrentScene;
        var spawnPosition = gameManager.GetComponent<SpawnPointRepository>().GetSpawnPoint(currentScene);

        Debug.LogFormat
        ("Spawning player in scene {0} on position [{1}]", currentScene.GetStringValue(),
            spawnPosition.position);

        var player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity);

        //Set default checkpoint in current scene
        player.GetComponent<Respawn>().currentCheckpoint = spawnPosition;

        Instantiate(uiPrefab);
    }
}