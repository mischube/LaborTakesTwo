using UnityEngine;

public class CannonballDmg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth == null)
                return;
            bossHealth.DamageBoss(3f);
            Destroy(gameObject);
        }
    }
}
