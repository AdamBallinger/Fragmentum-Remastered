using Scripts.Utils;
using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public class FlamethrowerAbilityController : AbilityController
    {
        [SerializeField]
        private RotateTo batRotator = null;

        [SerializeField]
        private RotateTo flamesRotator = null;

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

        public override void OnInitialize()
        {
            Animator?.SetBool("Roar", true);
            Animator?.SetBool("Flamethrower", true);

            t = 0.0f;
            distance = Vector3.Distance(GetFlamesStart(), GetFlamesEnd());
        }

        public override void OnStart()
        {
            flamethrowerPS?.Play(true);       
        }

        public override void AbilityUpdate()
        {
            var target = Vector3.Lerp(GetFlamesStart(), GetFlamesEnd(), flamesMoveCurve.Evaluate(t));

            flamesRotator.rotateTarget = target;
            batRotator.rotateTarget = target;

            t += Time.deltaTime / (distance / flamesRotationSpeed);
        }

        public override void OnFinish()
        {
            flamethrowerPS?.Stop(true);

            Animator?.SetBool("Roar", false);
            Animator?.SetBool("Flamethrower", false);
        }

        public override bool HasFinished()
        {
            return t >= 1.0f;
        }

        private Vector3 GetFlamesStart()
        {
            return flamesCenter + Vector3.left * flamesStartOffset;
        }

        private Vector3 GetFlamesEnd()
        {
            return flamesCenter + Vector3.right * flamesEndOffset;
        }

        private void OnDrawGizmos()
        {
            if(gizmosEnabled)
            {
                //Gizmos.color = Color.green;
                //Gizmos.DrawCube(flamesCenter + Vector3.left * flamesStartOffset, Vector3.one);
                //Gizmos.color = Color.green;
                //Gizmos.DrawCube(flamesCenter + Vector3.left * -flamesStartOffset, Vector3.one);

                //Gizmos.color = Color.red;
                //Gizmos.DrawCube(flamesCenter + Vector3.right * flamesEndOffset, Vector3.one);
                //Gizmos.DrawCube(flamesCenter + Vector3.right * -flamesEndOffset, Vector3.one);

                Gizmos.color = Color.green;
                Gizmos.DrawCube(GetFlamesStart(), Vector3.one);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(GetFlamesEnd(), Vector3.one);
            }
        }
    }
}
