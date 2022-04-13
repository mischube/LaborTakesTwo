using Photon.Pun;
using UnityEngine;

namespace Networking
{
    public class NetworkLauncher : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;

        private void Start()
        {
            Connect();
        }

        public void Connect()
        {
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
            Debug.Log("Connected now Join room");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No Room, Creating now");
            PhotonNetwork.CreateRoom("My Room", new Photon.Realtime.RoomOptions { MaxPlayers = 2, IsVisible = true });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Connect to room");
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(65, 16, -43), Quaternion.identity);
        }
    }
}