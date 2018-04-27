using Scripts.Combat;
using Scripts.Extensions;
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
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public void OnRadiusEnter(Collider _collider)
        {
            if(_collider.gameObject.CompareTag("Player"))
            {
                actionManager.SetActionImmediate(new ChaseAction(actionManager, player, moveSpeed));
                Animator?.SetBool("Walking", true);
            }
        }

        public void OnRadiusExit(Collider _collider)
        {
            if(_collider.gameObject.CompareTag("Player"))
            {
                actionManager.SetActionImmediate(new IdleAction(actionManager, 0.0f));
                Animator?.SetBool("Walking", false);
            }
        }

        public override void OnDeath()
        {
            gameObject.SetActive(false);
        }

        public AttackType[] GetResistances()
        {
            return new[] { AttackType.Player_Dash };
        }
    }
}
