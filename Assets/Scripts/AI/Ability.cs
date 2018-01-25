using UnityEngine;

namespace Scripts.AI
{
    [CreateAssetMenu(fileName = "Ability_", menuName = "Ability")]
    public class Ability : ScriptableObject
    {
        public new string name;
        public ParticleSystem particleSystem;
        public int damage;
    }
}
