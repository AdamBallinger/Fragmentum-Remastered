using UnityEngine;

namespace Scripts.Combat
{
    public class DestructibleObject : MonoBehaviour, IDamageable, IResistant
    {
        public HealthSystem GetHealth()
        {
            return null;
        }

        public void OnDamageReceived(int _damage)
        {
            gameObject.SetActive(false);
        }

        public void OnDeath() { }

        public AttackType[] GetResistances()
        {
            return new[] {AttackType.Head_Hit, AttackType.Projectile};
        }
    }
}