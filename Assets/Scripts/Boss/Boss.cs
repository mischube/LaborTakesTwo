using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float health = 3f;
    private float fireBallCd = 5f;
    private bool fireBallActive = false;
    private Collider[] cd;

    public GameObject fireball;
    public Transform middlePoint;

    private void Update()
    {
        if (!fireBallActive)
        {
            StartCoroutine(ShootFireBall());
        }
    }

    private IEnumerator ShootFireBall()
    {
        fireBallActive = true;
        cd = Physics.OverlapSphere(middlePoint.position, 20f, LayerMask.GetMask("Player"));
        if (cd != null)
        {
            foreach (var player in cd)
            {
                Instantiate(fireball, new Vector3(player.transform.position.x, 0.1f , player.transform.position.z), Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(fireBallCd);
        fireBallActive = false;
    }

    private void BossHit()
    {
        if (health <= 0)
        {
            BossDead();
        }
    }

    private void BossDead()
    {
        
    }
}