using Scripts.AI.Controllers;
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

        private float t, distance;

        public AttackDirection attackDirection = AttackDirection.Left;

        public override void OnInitialize()
        {
            Animator?.SetBool("Roar", true);
            Animator?.SetBool("Flamethrower", true);

            t = 0.0f;
            distance = Vector3.Distance(GetFlamesStart(), GetFlamesEnd());

            batAIController.AllowRotation = false;
            batAIController.Rotator.rotateTarget = GetFlamesStart();

            flamesRotator.rotateTarget = GetFlamesStart();
        }

        public override void OnStart()
        {
            flamethrowerPS?.Play(true);       
        }

        public override void AbilityUpdate()
        {
            var target = Vector3.Lerp(GetFlamesStart(), GetFlamesEnd(), flamesMoveCurve.Evaluate(t));

            flamesRotator.rotateTarget = target;
            batAIController.Rotator.rotateTarget = target;

            t += Time.deltaTime / (distance / flamesRotationSpeed);
        }

        public override void OnFinish()
        {
            flamethrowerPS?.Stop(true);

            Animator?.SetBool("Roar", false);
            Animator?.SetBool("Flamethrower", false);

            batAIController.AllowRotation = true;
        }

        public override bool HasFinished()
        {
            return t >= 1.0f;
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
        Left = -1,
        Right = 1
    }
}
