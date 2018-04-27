using Scripts.Extensions;
using UnityEngine;

namespace Scripts.AI
{
    public class ChaseAction : AIAction
    {
        private Transform target, transform;

        // Local reference for the AIController character controller.
        private CharacterController controller;

        private float speed;

        public ChaseAction(AIActionManager _actionManager, Transform _target, float _speed) : base(_actionManager)
        {
            target = _target;
            transform = ActionManager.Controller.transform;
            controller = ActionManager.Controller.Controller;
            speed = _speed;
        }

        public override void Update()
        {
            var direction = transform.position.DirectionTo(target.position);

            if(ActionManager.Controller.usesGravity)
            {
                // Simple move if the AI uses gravity has it will automatically be applied to the AI by the character controller.
                // Delta time is also automatically applied using simple move.
                controller.SimpleMove(direction * speed);
            }
            else
            {
                // Normal move if not as we don't want any gravity being automatically applied to the AI.
                controller.Move(direction * speed * Time.deltaTime);
            }
        }

        public override bool HasFinished()
        {
            // Chase action will run until its manually interrupted by the action manager.
            return false;
        }
    }
}
