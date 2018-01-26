using UnityEngine;

namespace Scripts.Abilities
{
    [CreateAssetMenu(fileName = "Ability_", menuName = "Ability Data")]
    public class AbilityData : ScriptableObject
    {
        public new string name;
        public ParticleSystem particleSystem;
        public float startDelay;
        public int damage;
    }
}
