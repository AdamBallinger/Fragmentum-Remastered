using UnityEngine;

namespace Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public int maxHealth = 100;

        [Tooltip("Time in seconds between damage being registered.")]
        public float damageCooldown = 1.0f;

        private float currentCooldownTime;

        private bool canTakeDamage = true;

        public int CurrentHealth { get; private set; }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
            currentCooldownTime = 0.0f;
            canTakeDamage = true;
        }

        private void Update()
        {
            if(!canTakeDamage)
            {
                if (currentCooldownTime >= damageCooldown)
                {
                    currentCooldownTime = 0.0f;
                    canTakeDamage = true;
                }

                currentCooldownTime += Time.deltaTime;             
            }
        }

        public void AddHealth(int _amt)
        {
            // prevent negative damage being added.
            _amt = Mathf.Abs(_amt);

            CurrentHealth += _amt;

            if(CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
        }

        public void RemoveHealth(int _amt)
        {
            // prevent negative health being removed.
            _amt = Mathf.Abs(_amt);

            if(canTakeDamage)
            {
                if(damageCooldown > 0.0f)
                {
                    canTakeDamage = false;
                }

                CurrentHealth -= _amt;
            }
        }
    }
}
