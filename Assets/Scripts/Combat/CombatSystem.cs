using System.Linq;
using UnityEngine;

namespace Scripts.Combat
{
    public static class CombatSystem
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
            var combatManager = _target.GetComponentInChildren<DamageInterface>();    
            
            if(damageProvider == null || combatManager == null)
            {
                return;
            }         

            var resistance = combatManager.Resistance;

            if(resistance != null)
            {
                if(resistance.GetResistances().ToList().Contains(_attackType))
                {
                    return;
                }
            }

            combatManager.Damageable.GetHealth()?.RemoveHealth(damageProvider.GetDamage());

            if(combatManager.Damageable.GetHealth()?.CurrentHealth <= 0)
            {
                combatManager.Damageable.OnDeath();
            }
            else
            {
                combatManager.Damageable.OnDamageReceived(damageProvider.GetDamage());
            }
        }
    }
}
