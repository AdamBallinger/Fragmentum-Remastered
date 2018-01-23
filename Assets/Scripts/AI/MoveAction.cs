using Scripts.AI.Controllers;
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

        public MoveAction(AIActionManager _actionManager, Vector3 _start, Vector3 _target, float _speed, AnimationCurve _moveCurve) : base(_actionManager)
        {
            initialPosition = _start;
            targetPosition = _target;
            distance = Vector3.Distance(initialPosition, targetPosition);
            moveSpeed = _speed;
            moveCurve = _moveCurve;
            t = 0.0f;
        }

        public override void Update(AIController _controller)
        {
            if(t >= 1.0f)
            {
                finished = true;
                return;
            }

            actionManager.controller.transform.position = Vector3.Lerp(initialPosition, targetPosition, moveCurve.Evaluate(t));

            t += Time.deltaTime / (distance / moveSpeed);
        }
    }
}
