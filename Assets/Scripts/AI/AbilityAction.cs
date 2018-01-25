using UnityEngine;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {

        private Ability ability;

        private Vector3 start, direction;

        public AbilityAction(AIActionManager _actionManager, Ability _ability, Vector3 _start, Vector3 _direction) : base(_actionManager)
        {
            ability = _ability;
            start = _start;
            direction = _direction;
        }

        public override void OnActionStart()
        {
            if (ability.particleSystem == null)
            {
                Debug.LogWarning($"Ability: {ability.name}, has no assigned particle system.");
            }
            else
            {
                Object.Instantiate(ability.particleSystem, start, Quaternion.identity);
            }
        }

        public override void Update()
        {

        }
    }
}
