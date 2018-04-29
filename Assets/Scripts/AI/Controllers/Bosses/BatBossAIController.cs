using Scripts.Abilities.Controllers;
using UnityEngine;

namespace Scripts.AI.Controllers.Bosses
{
    public class BatBossAIController : AIController
    {
        public bool drawGizmos = true;

        public float moveSpeed;

        public Vector3[] movePoints;

        public AbilityController flamethrowerAbility;
        public AbilityController basicAttackAbility;

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            var actionSequence = new AIActionSequence(true);
            actionSequence.AddActionToSequence(new MoveAction(ActionManager, movePoints[1], moveSpeed));
            actionSequence.AddActionToSequence(new MoveAction(ActionManager, movePoints[2], moveSpeed));
            actionSequence.AddActionToSequence(new AbilityAction(ActionManager, basicAttackAbility));
            actionSequence.AddActionToSequence(new MoveAction(ActionManager, movePoints[3], moveSpeed));
            actionSequence.AddActionToSequence(new MoveAction(ActionManager, movePoints[0], moveSpeed));
            actionSequence.AddActionToSequence(new AbilityAction(ActionManager, basicAttackAbility));
            actionSequence.AddActionToSequence(new MoveAction(ActionManager, movePoints[4], moveSpeed, true));
            actionSequence.AddActionToSequence(new AbilityAction(ActionManager, flamethrowerAbility));

            ActionManager.SetActionSequence(actionSequence);
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public override void OnDamageReceived(int _damage)
        {
            throw new System.NotImplementedException();
        }

        public override void OnDeath()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            foreach(var point in movePoints)
            {
                Gizmos.DrawSphere(point, 1.0f);
            }
        }
    }
}