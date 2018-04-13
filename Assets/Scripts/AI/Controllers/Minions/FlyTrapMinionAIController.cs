using Scripts.Abilities.Controllers;
using UnityEngine;

namespace Scripts.AI.Controllers.Minions
{
    public class FlyTrapMinionAIController : AIController
    {
        public float attackDelay = 1.0f;
        public AbilityController abilityController;

        private Transform player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            var sequence = new AIActionSequence(true);
            sequence.AddActionToSequence(new IdleAction(actionManager, attackDelay));
            sequence.AddActionToSequence(new AbilityAction(actionManager, abilityController));

            actionManager.SetActionSequence(sequence);
        }

        protected override void ControllerUpdate()
        {
            
        }
    }
}
