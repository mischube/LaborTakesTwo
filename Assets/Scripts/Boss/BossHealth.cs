using Boss;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public event Victory victoryEvent;
    
    [SerializeField]
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
        victoryEvent?.Invoke();
        //switch to wining scene or play cutscene
    }
}
