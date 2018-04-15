using UnityEngine;

namespace Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public int maxHealth = 100;

        public int CurrentHealth { get; private set; }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
        }

        public void AddHealth(int _amt)
        {
            CurrentHealth += _amt;

            if(CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
        }

        public void RemoveHealth(int _amt)
        {
            CurrentHealth -= _amt;
        }
    }
}
