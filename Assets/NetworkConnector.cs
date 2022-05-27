using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkConnector : MonoBehaviourPunCallbacks
{
    public event JoinedRoomHandler JoinedRoom;
    public event ConnectedHandler Connected;

    public bool JoinRandomRoom { get; set; }

    public override void OnEnable()
    {
        base.OnEnable();

        DontDestroyOnLoad(this);
    }

    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 60;

        if (PhotonNetwork.IsConnected)
            return;

        Debug.Log("Not connected. connecting now");
        PhotonNetwork.ConnectUsingSettings();
    }


    public void JoinRoom()
    {
        JoinRandomRoom = true;

        if (JoinRandomRoom)
        {
            Debug.Log("Joining random room..");
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions { MaxPlayers = 2, IsVisible = true });
            return;
        }

        Debug.Log("Joining 'TestRoom'");

        PhotonNetwork.JoinOrCreateRoom(
            "TestRoom",
            new RoomOptions { MaxPlayers = 2, IsVisible = true },
            TypedLobby.Default);
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        Connected?.Invoke();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"Failed to join a random room | message: [{message}]");
    }


    public override void OnJoinedRoom()
    {
        Debug.LogFormat("Joined room [{0}]", PhotonNetwork.CurrentRoom.Name);
        JoinedRoom?.Invoke();

        Destroy(gameObject);
    }
}

public delegate void JoinedRoomHandler();

public delegate void ConnectedHandler();
