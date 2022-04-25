using System.Collections;
using UnityEngine;

public class BossMeteor : MonoBehaviour
{
    private float health = 3f;
    private float fireBallCd = 5f;
    private bool fireBallActive = false;

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
        Collider [] cd = Physics.OverlapSphere(middlePoint.position, 20f, LayerMask.GetMask("Player"));
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

    private void HandControll()
    {
        //reference to hand script
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
