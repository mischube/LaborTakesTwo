using System.Collections;
using Player;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject body;

    public event PlayerDamaged playerDmgEvent;
    public event PlayerDead playerDeadEvent;
    
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
            
            currentHealth -= dmg;
            
            playerDmgEvent?.Invoke((int)currentHealth);

            if (currentHealth <= 0f)
            {
                PlayerDeath();
            }
        }
    }

    private void PlayerDeath()
    {
        currentHealth = maxHealth;
        playerDeadEvent?.Invoke();
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

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
