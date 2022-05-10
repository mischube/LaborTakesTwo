using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private float maxHealth = 3;
        [SerializeField] private float invincibilityTime = 2f;

        public event PlayerDamaged PlayerDmgEvent;
        public event PlayerDead PlayerDeadEvent;


        private float _currentHealth;
        private bool _gettingDamaged;


        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void DamagePlayer(float dmg)
        {
            if (!_gettingDamaged)
            {
                StartCoroutine(PlayerInvincibility());

                _currentHealth -= dmg;

                PlayerDmgEvent?.Invoke((int)_currentHealth);

                if (_currentHealth <= 0f)
                {
                    PlayerDeath();
                }
            }
        }

        private void PlayerDeath()
        {
            _currentHealth = maxHealth;
            PlayerDeadEvent?.Invoke();
        }

        IEnumerator PlayerInvincibility()
        {
            var oldColor = body.GetComponent<Renderer>().material.color;

            _gettingDamaged = true;
            body.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(invincibilityTime);
            body.GetComponent<Renderer>().material.color = oldColor;
            _gettingDamaged = false;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
    }
}