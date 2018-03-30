using Scripts.Utils;
using UnityEngine;

namespace Scripts.AI
{
    public class MoveAction : AIAction
    {
        private Vector3 targetPosition;

        private Interpolator interpolator;

        /// <summary>
        /// Determines if the action will also rotate the AI towards the direction it is moving.
        /// </summary>
        private bool rotateTowards;

        /// <summary>
        /// Create a new move action for an AI agent.
        /// </summary>
        /// <param name="_actionManager">AI action manager.</param>
        /// <param name="_start">Position the AI starts moving form.</param>
        /// <param name="_target">Position the AI moves toward.</param>
        /// <param name="_speed">The speed the AI moves at.</param>
        /// <param name="_moveCurve">The curve to apply to movement for smoothing.</param>
        /// <param name="_rotateTowards">Controls if the move action overrides the AI rotation to look at the target position.</param>
        public MoveAction(AIActionManager _actionManager, Vector3 _start, Vector3 _target, float _speed,
            AnimationCurve _moveCurve, bool _rotateTowards = false) : base(_actionManager)
        {
            targetPosition = _target;
            var moveDistance = Vector3.Distance(_start, targetPosition);
            interpolator = new Interpolator(_start, targetPosition, moveDistance / _speed, _moveCurve);
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
            if(interpolator.HasFinished())
            {
                finished = true;
                return;
            }

            ActionManager.Controller.transform.position = interpolator.Interpolate();

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

            // Reset the action for when its used in a repeating sequence.
            finished = false;
            interpolator.Reset();
        }
    }
}
