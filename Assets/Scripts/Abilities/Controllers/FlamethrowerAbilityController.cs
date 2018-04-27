using Scripts.AI.Controllers;
using Scripts.Extensions;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public class FlamethrowerAbilityController : AbilityController
    {
        [SerializeField]
        private AIController batAIController = null;

        [SerializeField]
        private Rotator flamesRotator = null;

        [SerializeField]
        private ParticleSystem flamethrowerPS = null;

        [SerializeField]
        private AnimationCurve flamesMoveCurve = null;

        [SerializeField]
        private float flamesRotationSpeed = 1.0f;

        [SerializeField]
        private Vector3 flamesCenter = Vector3.zero;

        [SerializeField]
        private float flamesStartOffset = 0.0f;

        [SerializeField]
        private float flamesEndOffset = 0.0f;

        private Vector3Interpolator vector3Interpolator;

        public AttackDirection attackDirection = AttackDirection.Left;

        public override void OnPreStart()
        {
            Animator?.SetBool("Roar", true);
            Animator?.SetBool("Flamethrower", true);

            var direction = batAIController.transform.position.DirectionToPlayer();
            var dot = Vector3.Dot(flamesCenter, direction);

            attackDirection = dot < 0.0f ? AttackDirection.Left : AttackDirection.Right;    

            vector3Interpolator = new Vector3Interpolator(GetFlamesStart(), GetFlamesEnd(), flamesRotationSpeed, flamesMoveCurve);

            batAIController.ControlsRotation = false;
            batAIController.Rotator.rotateTarget = GetFlamesStart();

            flamesRotator.rotateTarget = GetFlamesStart();
        }

        public override void OnStart()
        {
            flamethrowerPS?.Play(true);       
        }

        public override void OnUpdate()
        {
            var target = vector3Interpolator.Interpolate();

            flamesRotator.rotateTarget = target;
            batAIController.Rotator.rotateTarget = target;
        }

        public override void OnFinish()
        {
            flamethrowerPS?.Stop(true);

            Animator?.SetBool("Roar", false);
            Animator?.SetBool("Flamethrower", false);

            batAIController.ControlsRotation = true;
        }

        public override bool HasFinished()
        {
            return vector3Interpolator.HasFinished();
        }

        /// <summary>
        /// Gets the flames starting position based on the ability attack direction.
        /// </summary>
        /// <returns></returns>
        private Vector3 GetFlamesStart()
        {
            return flamesCenter + Vector3.left * (int)attackDirection * flamesStartOffset;
        }

        private Vector3 GetFlamesEnd()
        {
            return flamesCenter + Vector3.right * (int)attackDirection * flamesEndOffset;
        }

        private void OnDrawGizmos()
        {
            if(gizmosEnabled)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(GetFlamesStart(), Vector3.one);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(GetFlamesEnd(), Vector3.one);
            }
        }
    }

    public enum AttackDirection
    {
        Left = 1,
        Right = -1
    }
}
