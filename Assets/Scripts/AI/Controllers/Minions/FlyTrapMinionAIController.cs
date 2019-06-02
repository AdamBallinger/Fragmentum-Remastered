using Scripts.Abilities.Controllers;
using UnityEngine;

namespace Scripts.AI.Controllers.Minions
{
    public class FlyTrapMinionAIController : AIController
    {
        [Header("Pop-out Settings.")]
        public float popoutSpeed = 1.0f;
        public float popoutDist = 1.0f;
        private bool hasPoped;

        [Header("Attack Settings.")]
        public float attackDelay = 1.0f;
        public AbilityController abilityController;

        private bool playerInRange;

        private Transform player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            ActionManager.StopSequence();

            var sequence = new AIActionSequence(true);
            sequence.AddActionToSequence(new IdleAction(ActionManager, attackDelay));
            sequence.AddActionToSequence(new AbilityAction(ActionManager, abilityController));

            ActionManager.SetActionSequence(sequence);
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public void OnRadiusTriggerEnter(Collider _collider)
        {
            if(_collider.gameObject.CompareTag("Player"))
            {
                playerInRange = true;

                if (!hasPoped)
                {
                    hasPoped = true;
                    Animator?.SetBool("Roaring", true);
                    ActionManager.SetActionImmediate(new MoveAction(ActionManager, transform.position + Vector3.up * popoutDist,
                        popoutSpeed));
                }
                else
                {
                    ActionManager.ResumeSequence();
                }         
            }
        }

        public void OnRadiusTriggerExit(Collider _collider)
        {           
            if(_collider.gameObject.CompareTag("Player"))
            {
                playerInRange = false;

                if (hasPoped)
                {
                    if(ActionManager.GetSequence()?.GetActiveAction() is AbilityAction)
                    {
                        ActionManager.StopSequence(SequenceStopBehaviour.ForceFinish);
                    }
                    else
                    {
                        ActionManager.StopSequence();
                    }
                }              
            }
        }

        public override void OnManagerActionFinished(AIAction _action)
        {
            if(_action is MoveAction)
            {
                Animator?.SetBool("Roaring", false);

                if(playerInRange)
                {
                    ActionManager.ResumeSequence();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(GetComponent<Transform>().position + Vector3.up * popoutDist, Vector3.one * 0.5f);
        }

        public override void OnDeath()
        {
            base.OnDeath();
            
            // TODO: Add particles to death.
        }
    }
}
