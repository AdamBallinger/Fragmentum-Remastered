using EzPool;
using Scripts.Extensions;
using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    [RequireComponent(typeof(EzPoolManager))]
    public class ProjectileAbilityController : AbilityController
    {
        [SerializeField]
        private float duration = 1.0f;

        private float currentDuration;

        [SerializeField]
        private Vector3 spawnOffset = Vector3.zero;

        [SerializeField]
        private EzPoolManager projectilePool = null;

        [Tooltip("Animator parameter to use when firing the projectile.")]
        [SerializeField]
        private string animationParameter = string.Empty;

        [SerializeField]
        private LayerMask collidable = 0;

        private GameObject projectile;

        public override void OnPreStart()
        {
            Animator?.SetBool(animationParameter, true);  
        }

        public override void OnStart()
        {
            currentDuration = 0.0f;
            projectile = projectilePool.GetAvailable();
            projectile.transform.position = transform.TransformPoint(spawnOffset);
            var pc = projectile.GetComponent<ProjectileController>();
            pc.Direction = projectile.transform.position.DirectionToPlayer();
            pc.CollisionMask = collidable;
        }

        public override void OnUpdate()
        {
            currentDuration += Time.deltaTime;
        }

        public override void OnFinish()
        {
            Animator?.SetBool(animationParameter, false);
        }

        public override bool HasFinished()
        {
            return currentDuration >= duration;
        }

        private void OnDrawGizmos()
        {
            if(!gizmosEnabled)
            {
                return;
            }

            var projectileStart = transform.TransformPoint(spawnOffset);

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(projectileStart, Vector3.one / 2.0f);
        }
    }
}
