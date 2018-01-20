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
        /// Time in seconds to spend moving to the given target position.
        /// </summary>
        private float moveTime;

        /// <summary>
        /// Linear interpolation parameter.
        /// </summary>
        private float t;

        public MoveAction(AIBrain _brain, Vector3 _target, float _time, AnimationCurve _moveCurve) : base(_brain)
        {
            initialPosition = brain.controller.transform.position;
            targetPosition = _target;
            moveTime = _time;
            moveCurve = _moveCurve;
            t = 0.0f;
        }

        public override void Update(GenericAIController _controller)
        {
            if(t >= 1.0f)
            {
                brain.OnActionFinished();
                return;
            }

            brain.controller.transform.position = Vector3.Lerp(initialPosition, targetPosition, moveCurve.Evaluate(t));

            t += Time.deltaTime / moveTime;
        }
    }
}
