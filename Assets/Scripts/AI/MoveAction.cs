using UnityEngine;

namespace Scripts.AI
{
    public class MoveAction : AIAction
    {

        private Vector3 initialPosition;
        private Vector3 targetPosition;

        /// <summary>
        /// The animation curve to sample when interpolating movement.
        /// </summary>
        private AnimationCurve moveCurve;

        /// <summary>
        /// Speed in which to move towards the target position.
        /// </summary>
        private float moveSpeed;

        /// <summary>
        /// Distance from initial to target position.
        /// </summary>
        private float distance;

        /// <summary>
        /// Linear interpolation parameter.
        /// </summary>
        private float t;

        /// <summary>
        /// Determines if the action will also rotate the AI towards the direction it is moving.
        /// </summary>
        private bool rotateTowards;

        public MoveAction(AIActionManager _actionManager, Vector3 _start, Vector3 _target, float _speed, AnimationCurve _moveCurve,
            bool _rotateTowards = false) : base(_actionManager)
        {
            initialPosition = _start;
            targetPosition = _target;
            distance = Vector3.Distance(initialPosition, targetPosition);
            moveSpeed = _speed;
            moveCurve = _moveCurve;
            t = 0.0f;
            rotateTowards = _rotateTowards;
        }

        public override void OnActionStart()
        {
            if (rotateTowards)
            {
                ActionManager.Controller.ControlsRotation = false;
            }
        }

        public override void Update()
        {
            if(t >= 1.0f)
            {
                finished = true;
                return;
            }

            ActionManager.Controller.transform.position = Vector3.Lerp(initialPosition, targetPosition, moveCurve.Evaluate(t));

            if(rotateTowards)
            {
                ActionManager.Controller.Rotator.rotateTarget = targetPosition;
            }
            
            t += Time.deltaTime / (distance / moveSpeed);
        }

        public override void OnActionFinished()
        {
            if(rotateTowards)
            {
                ActionManager.Controller.ControlsRotation = true;
            }
        }
    }
}
