using UnityEngine;

public class DmgBossMeteor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            bossHealth.DamageBoss(1f);
            Destroy(gameObject);
        }
    }
}
