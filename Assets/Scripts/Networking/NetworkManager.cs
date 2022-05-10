using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public event JoinedLobbyHandler OnLobbyJoined;


        public void Connect()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("joining random room");
                PhotonNetwork.JoinRandomRoom();
            } else
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
            Debug.LogFormat("Joined room [{0}]", PhotonNetwork.CurrentRoom.Name);
            OnLobbyJoined?.Invoke();
        }
    }
}