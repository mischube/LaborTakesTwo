using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private float health = 3f;

    public void DamageBoss(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            BossDeath();
        }
    }

    private void BossDeath()
    {
        Debug.Log("Boss died. You win !!!!!");
        //switch to wining scene or play cutscene
    }
}
