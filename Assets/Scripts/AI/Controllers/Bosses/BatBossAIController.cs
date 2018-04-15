using Scripts.Abilities.Controllers;
using UnityEngine;

namespace Scripts.AI.Controllers.Bosses
{
    public class BatBossAIController : AIController
    {
        public bool drawGizmos = true;

        public AnimationCurve moveCurve;
        public float moveSpeed;

        public Vector3[] movePoints;

        public AbilityController flamethrowerAbility;
        public AbilityController basicAttackAbility;

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            var actionSequence = new AIActionSequence(true);
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[1], moveSpeed, moveCurve));
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[2], moveSpeed, moveCurve));
            actionSequence.AddActionToSequence(new AbilityAction(actionManager, basicAttackAbility));
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[3], moveSpeed, moveCurve));
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[0], moveSpeed, moveCurve));
            actionSequence.AddActionToSequence(new AbilityAction(actionManager, basicAttackAbility));
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[4], moveSpeed, moveCurve, true));
            actionSequence.AddActionToSequence(new AbilityAction(actionManager, flamethrowerAbility));

            actionManager.SetActionSequence(actionSequence);
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