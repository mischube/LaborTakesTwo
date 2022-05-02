using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform cannonBallShootingPosition;

    public void ShootCannonBall()
    {
        PhotonNetwork.Instantiate
            (cannonBallPrefab.name, cannonBallShootingPosition.position, transform.rotation);
    }
}
