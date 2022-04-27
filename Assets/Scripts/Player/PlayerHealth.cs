using System.Collections;
using Player;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject body;
    
    private AutoRespawn autoRespawn;
    private CharacterController characterController;
    private float currentHealth;
    private float maxHealth = 3;
    private float invincibilityTime = 2f;
    private bool gettingDamaged;

    private void Start()
    {
        autoRespawn = GetComponent<AutoRespawn>();
        characterController = GetComponent<CharacterController>();

        currentHealth = maxHealth;
    }

    public void DamagePlayer(float dmg)
    {
        if (!gettingDamaged)
        {
            StartCoroutine(PlayerInvincibility());
            
            Debug.Log(currentHealth);
            currentHealth -= dmg;

            if (currentHealth <= 0f)
            {
                PlayerDeath();
            }
        }
    }

    private void PlayerDeath()
    {
        characterController.enabled = false;
        transform.position = autoRespawn.respawnPoint;
        characterController.enabled = true;
        currentHealth = maxHealth;
    }

    IEnumerator PlayerInvincibility()
    {
        var oldColor = body.GetComponent<Renderer>().material.color;
        
        gettingDamaged = true;
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(invincibilityTime);
        body.GetComponent<Renderer>().material.color = oldColor;
        gettingDamaged = false;
    }
}
