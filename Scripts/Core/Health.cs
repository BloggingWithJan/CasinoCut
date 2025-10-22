using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CasinoCut.Core
{
    public class Health : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth { get; private set; }

        public event Action<int> OnTakeDamage;
        public event Action<int> OnHeal;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            int damage = (int)Math.Ceiling(amount);
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            Debug.Log(
                $"{gameObject.name} took {damage} damage. Current health: {currentHealth}/{maxHealth}"
            );
            OnTakeDamage?.Invoke(damage);
            if (currentHealth == 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            int healAmount = (int)Math.Ceiling(amount);
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
            OnHeal?.Invoke(healAmount);
            Debug.Log(
                $"{gameObject.name} healed {healAmount} health. Current health: {currentHealth}/{maxHealth}"
            );
        }

        private void Die()
        {
            if (isDead)
                return;

            Debug.Log($"{gameObject.name} has died.");
            isDead = true;
            GetComponent<Animator>()?.SetTrigger("die");
        }
    }
}
