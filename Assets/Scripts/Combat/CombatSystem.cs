using System.Linq;
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
        /// <param name="_attackType"></param>
        public static void ProcessDamage(GameObject _source, GameObject _target, AttackType _attackType)
        {
            var damageProvider = _source.GetComponent<IDamageProvider>();
            var damageable = _target.GetComponent<IDamageable>();

            if(damageProvider == null || damageable == null)
            {
                return;
            }

            var resistance = _target.GetComponent<IResistant>();

            if(resistance != null)
            {
                if(resistance.GetResistances().ToList().Contains(_attackType))
                {
                    return;
                }
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
