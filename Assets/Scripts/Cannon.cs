using Photon.Pun;
using UnityEngine;

public class Cannon : MonoBehaviourPun
{
    public GameObject cannonBallPrefab;
    public Transform cannonBallShootingPosition;

    public void ShootCannonBall()
    {
        if (!photonView.IsMine)
            return;
        
        PhotonNetwork.Instantiate
            (cannonBallPrefab.name, cannonBallShootingPosition.position, transform.rotation);
    }
}
