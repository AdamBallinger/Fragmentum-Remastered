using UnityEngine;

namespace Scripts.Combat
{
    public class CombatSystem
    {
        /// <summary>
        /// Process damage for a target from a source.
        /// </summary>
        /// <param name="_source"></param>
        /// <param name="_target"></param>
        public static void ProcessDamage(GameObject _source, GameObject _target)
        {
            var damageProvider = _source.GetComponent<IDamageProvider>();
            var damageable = _target.GetComponent<IDamageable>();

            if(damageProvider == null || damageable == null)
            {
                return;
            }

            damageable.GetHealth().RemoveHealth(damageProvider.GetDamage());

            if(damageable.GetHealth().CurrentHealth <= 0)
            {
                damageable.OnDeath();
            }
            else
            {
                damageable.OnDamageReceived(damageProvider.GetDamage());
            }
        }
    }
}
