using UnityEngine;

namespace Scripts.Combat
{
    /// <summary>
    /// Interfaces sub colliders in an object with the damage interfaces.
    /// For example, the mushroom minion head collider needs to be able to tell the combat system where the IDamageable and
    /// IResistant interfaces are found withing the minions object hierarchy.
    /// </summary>
    public class DamageInterface : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The gameobject that holds the script with the IDamageable and IResistant interfaces.")]
        private GameObject interfaceContainer = null;
        
        public IDamageable Damageable { get; private set; }
        public IResistant Resistance { get; private set; }

        private void Start()
        {
            Damageable = interfaceContainer.GetComponent<IDamageable>();
            Resistance = interfaceContainer.GetComponent<IResistant>();
        }
    }
}