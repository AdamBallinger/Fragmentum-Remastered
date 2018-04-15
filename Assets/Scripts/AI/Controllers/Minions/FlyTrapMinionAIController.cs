using Scripts.Abilities.Controllers;
using Scripts.Combat;
using UnityEngine;

namespace Scripts.AI.Controllers.Minions
{
    public class FlyTrapMinionAIController : AIController
    {
        [Header("Pop-out Settings.")]
        public float popoutSpeed = 1.0f;
        public float popoutDist = 1.0f;
        public AnimationCurve popoutCurve;
        private bool hasPoped;

        [Header("Attack Settings.")]
        public float attackDelay = 1.0f;
        public AbilityController abilityController;

        private Transform player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            actionManager.ToggleSequence(false);

            var sequence = new AIActionSequence(true);
            sequence.AddActionToSequence(new IdleAction(actionManager, attackDelay));
            sequence.AddActionToSequence(new AbilityAction(actionManager, abilityController));

            actionManager.SetActionSequence(sequence);
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public void OnRadiusTriggerEnter(Collider _collider)
        {
            if(!hasPoped && _collider.gameObject.CompareTag("Player"))
            {
                hasPoped = true;
                Animator?.SetBool("Roaring", true);
                actionManager.SetActionImmediate(new MoveAction(actionManager, transform.position + Vector3.up * popoutDist, popoutSpeed,
                    popoutCurve));
            }
        }

        public override void OnManagerActionFinished(AIAction _action)
        {
            if(_action is MoveAction)
            {
                Animator?.SetBool("Roaring", false);
                actionManager.ToggleSequence(true);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(GetComponent<Transform>().position + Vector3.up * popoutDist, Vector3.one * 0.5f);
        }

        public override void OnDamageReceived(int _damage)
        {
            throw new System.NotImplementedException();
        }

        public override void OnDeath()
        {
            throw new System.NotImplementedException();
        }
    }
}
