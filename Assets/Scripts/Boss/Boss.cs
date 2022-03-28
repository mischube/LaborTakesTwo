using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float health = 3f;
    private float fireBallCd = 5f;
    private bool fireBallActive = false;

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
