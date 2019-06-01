using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Combat
{
    public class DestructibleObject : MonoBehaviour, IDamageable, IResistant
    {
        [SerializeField]
        private UnityEvent onObjectDestroyed = null;
        
        public HealthSystem GetHealth()
        {
            return null;
        }

        public void OnDamageReceived(int _damage)
        {
            onObjectDestroyed?.Invoke();
            gameObject.SetActive(false);
        }

        public void OnDeath() { }

        public AttackType[] GetResistances()
        {
            return new[] {AttackType.Head_Hit, AttackType.Projectile};
        }
    }
}