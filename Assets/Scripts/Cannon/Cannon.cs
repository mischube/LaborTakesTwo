using Photon.Pun;
using UnityEngine;

public class Cannon : MonoBehaviourPun
{
    public GameObject cannonBallPrefab;
    public Transform cannonBallShootingPosition;

    public void ShootCannonBall()
    {
        Instantiate(cannonBallPrefab, cannonBallShootingPosition.position, transform.rotation);
    }
}
