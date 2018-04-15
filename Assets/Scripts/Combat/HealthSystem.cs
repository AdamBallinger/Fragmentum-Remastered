using UnityEngine;

namespace Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public int maxHealth = 100;

        public int CurrentHealth { get; set; }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
        }
    }
}
