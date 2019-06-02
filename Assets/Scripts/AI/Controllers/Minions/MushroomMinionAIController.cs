using Scripts.Combat;
using UnityEngine;

namespace Scripts.AI.Controllers.Minions
{
    public class MushroomMinionAIController : AIController, IResistant
    {
        public float moveSpeed = 1.0f;

        private Transform player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            ActionManager.SetDefaultAIAction(new IdleAction(ActionManager, 0.0f));
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public void OnRadiusEnter(Collider _collider)
        {
            if(_collider.gameObject.CompareTag("Player"))
            {
                ActionManager.SetActionImmediate(new ChaseAction(ActionManager, player, moveSpeed));
                Animator?.SetBool("Walking", true);
            }
        }

        public void OnRadiusExit(Collider _collider)
        {
            if(_collider.gameObject.CompareTag("Player"))
            {
                ActionManager.SetActionImmediate(null);
                Animator?.SetBool("Walking", false);
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();
            
            // TODO: Add particles on death
        }

        public AttackType[] GetResistances()
        {
            return new[] { AttackType.Player_Dash };
        }
    }
}
