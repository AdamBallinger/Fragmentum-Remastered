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

        public MoveAction(AIActionManager _actionManager, Vector3 _start, Vector3 _target, float _speed, AnimationCurve _moveCurve,
            bool _rotateTowards = false) : base(_actionManager)
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
        }
    }
}
