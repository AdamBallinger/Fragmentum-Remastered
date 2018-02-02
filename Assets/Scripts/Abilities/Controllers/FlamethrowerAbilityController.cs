using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public class FlamethrowerAbilityController : AbilityController
    {
        [SerializeField]
        private ParticleSystem flamethrowerPS = null;

        [SerializeField]
        private float flamesSpeed = 1.0f;

        [SerializeField]
        private Vector3 flamesCenter = Vector3.zero;

        [SerializeField]
        private float flameStartOffset = 0.0f;

        [SerializeField]
        private float flamesEndOffset = 0.0f;

        public override void OnAbilityStart()
        {
            flamethrowerPS?.Play(true);

            Animator?.SetBool("Roar", true);
            Animator?.SetBool("Flamethrower", true);
        }

        public override void AbilityUpdate()
        {
            
        }

        public override void OnAbilityFinished()
        {
            flamethrowerPS?.Stop(true);

            Animator?.SetBool("Roar", false);
            Animator?.SetBool("Flamethrower", false);
        }

        public override bool HasFinished()
        {
            return false;
        }

        private void OnDrawGizmos()
        {
            if(gizmosEnabled)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(flamesCenter + Vector3.left * flameStartOffset, Vector3.one);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(flamesCenter + Vector3.left * -flameStartOffset, Vector3.one);

                Gizmos.color = Color.red;
                Gizmos.DrawCube(flamesCenter + Vector3.right * flamesEndOffset, Vector3.one);
                Gizmos.DrawCube(flamesCenter + Vector3.right * -flamesEndOffset, Vector3.one);
            }
        }
    }
}
