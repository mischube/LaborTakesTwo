using Global;
using Library.StringEnums;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;

        public event JoinedLobbyHandler OnLobbyJoined;

        public void Connect()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("joining random room");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.Log("Not connected. connecting now");
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master. Joining room now");
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions { MaxPlayers = 2, IsVisible = true });
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"Failed to join a random room | message: [{message}]");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Connect to room [{PhotonNetwork.CurrentRoom.Name}]");
            OnLobbyJoined?.Invoke(this);
        }

        public void LoadScene(Scenes levelName)
        {
            //Secures that every player has same scene loaded
            // if (!PhotonNetwork.IsMasterClient)
                // return;

            Debug.LogFormat("Loading scene [{0}]", levelName.GetStringValue());

            PhotonNetwork.LoadLevel(levelName.GetStringValue());
        }

        public GameObject SpawnPlayer(Vector3 position)
        {
            Debug.LogFormat("Spawning player in scene {0} on position [{1}]", SceneManager.GetActiveScene().name,
                position);
            return PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        }
    }
}