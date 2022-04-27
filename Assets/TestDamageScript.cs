using UnityEngine;

public class TestDamageScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(1f);
        }
    }
}
