using Scripts.Extensions;
using UnityEngine;

namespace Scripts.AI
{
    public class MoveAction : AIAction
    {
        // Local reference to the AIController character controller.
        private CharacterController controller;

        private Vector3 targetPosition;

        private float speed;

        /// <summary>
        /// Local variables to track the time it takes to actually complete this action.
        /// </summary>
        private float currentMoveTime, timeToMove;

        /// <summary>
        /// Determines if the action will also rotate the AI towards the direction it is moving.
        /// </summary>
        private bool rotateTowards;

        /// <summary>
        /// Create a new move action for an AI agent.
        /// </summary>
        /// <param name="_actionManager">AI action manager.</param>
        /// <param name="_target">Position the AI moves toward.</param>
        /// <param name="_speed">The speed the AI moves at.</param>
        /// <param name="_rotateTowards">Controls if the move action overrides the AI rotation to look at the target position.</param>
        public MoveAction(AIActionManager _actionManager, Vector3 _target, float _speed, bool _rotateTowards = false) : base(_actionManager)
        {
            controller = _actionManager.Controller.Controller;
            targetPosition = _target;
            speed = _speed;
            rotateTowards = _rotateTowards;
        }

        public override void OnActionStart()
        {
            currentMoveTime = 0.0f;
            timeToMove = Vector3.Distance(ActionManager.Controller.transform.position, targetPosition) / speed;

            if (rotateTowards)
            {
                ActionManager.Controller.ControlsRotation = false;
            }
        }

        public override void Update()
        {
            currentMoveTime += Time.deltaTime;

            var direction = ActionManager.Controller.transform.position.DirectionTo(targetPosition);

            if(ActionManager.Controller.usesGravity)
            {
                controller.SimpleMove(direction * speed);
            }
            else
            {
                controller.Move(direction * speed * Time.deltaTime);
            }

            if(rotateTowards)
            {
                ActionManager.Controller.Rotator.rotateTarget = targetPosition;
            }
        }

        public override void OnActionFinished() 
        {
            if(rotateTowards)
            {
                ActionManager.Controller.ControlsRotation = true;
            }
        }

        public override bool HasFinished()
        {
            return currentMoveTime >= timeToMove;
        }
    }
}
