using Scripts.Abilities.Controllers;
using Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Scripts.AI.Controllers
{
    public class BatBossAIController : AIController
    {
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
            //actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[2], moveSpeed, moveCurve));
            //actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[3], moveSpeed, moveCurve));
            //actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[0], moveSpeed, moveCurve));
            actionSequence.AddActionToSequence(new MoveAction(actionManager, movePoints[4], moveSpeed, moveCurve, true));
            actionSequence.AddActionToSequence(new AbilityAction(actionManager, flamethrowerAbility));

            actionManager.SetActionSequence(actionSequence);

            //actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));

            //actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(150.0f, 17.0f, 11.0f),
            //    moveSpeed, moveCurve, true));
            //actionManager.EnqueAction(new AbilityAction(actionManager, flamethrowerAbility));

            //actionManager.EnqueAction(new IdleAction(actionManager, 1.0f));
            //actionManager.EnqueAction(new AbilityAction(actionManager, basicAttackAbility));
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);    
            

        }

        private void OnDrawGizmos()
        {
            foreach(var point in movePoints)
            {
                Gizmos.DrawSphere(point, 1.0f);
            }
        }
    }
}