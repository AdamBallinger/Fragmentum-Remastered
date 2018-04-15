using UnityEngine;

namespace Scripts.Combat
{
    public interface IDamageable
    {
        HealthSystem GetHealth();

        /// <summary>
        /// Called after damage has been applied to the health system and only if the host wasn't killed by the damage.
        /// </summary>
        /// <param name="_damage"></param>
        void OnDamageReceived(int _damage);

        /// <summary>
        /// Called when the health system reaches 0 or less health.
        /// </summary>
        void OnDeath();

    }
}
