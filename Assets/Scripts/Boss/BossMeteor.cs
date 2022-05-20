using System.Collections;
using Photon.Pun;
using UnityEngine;

public class BossMeteor : MonoBehaviourPun
{
    public GameObject fireball;
    public Transform middlePoint;
    
    private float fireBallCd = 5f;
    private bool _fireBallActive = false;
    [SerializeField]
    private float meteorActivationRange = 30f;

    private void Update()
    {
        if (!photonView.IsMine)
            return;
        
        if (!_fireBallActive)
        {
            StartCoroutine(ShootFireBall());
        }
    }

    private IEnumerator ShootFireBall()
    {
        _fireBallActive = true;
        Collider [] cd = Physics.OverlapSphere(middlePoint.position, meteorActivationRange, LayerMask.GetMask("Player"));
        if (cd != null)
        {
            foreach (var player in cd)
            {
                PhotonNetwork.Instantiate(fireball.name,
                    new Vector3(player.transform.position.x, 0.1f , player.transform.position.z), Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(fireBallCd);
        _fireBallActive = false;
    }
}
