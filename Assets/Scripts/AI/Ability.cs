using UnityEngine;

namespace Scripts.AI
{
    [CreateAssetMenu(fileName = "Ability_", menuName = "Ability")]
    public class Ability : ScriptableObject
    {
        public ParticleSystem particleSystem;
        public int damage;
    }
}
