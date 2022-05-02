using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            Debug.Log("activate catapult");
            animator.SetTrigger("activateCatapult");
        }
    }
}
