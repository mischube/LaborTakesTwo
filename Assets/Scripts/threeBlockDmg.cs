using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeBlockDmg : MonoBehaviour
{
    [SerializeField]
    private BossHealth bossHealth;
    
    private int counter = 0;

    public void setCounterUp()
    {
        counter++;
        checkCounter();
    }

    private void checkCounter()
    {
        if (counter == 3)
        {
            bossHealth.DamageBoss(1f);
        }
    }
}
