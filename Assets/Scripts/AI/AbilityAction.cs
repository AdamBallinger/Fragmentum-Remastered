using UnityEngine;
using Scripts.Abilities.Controllers;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {
        public AbilityController AbilityController { get; }

        private float currentDelayTime;

        private bool abilityStarted;

        public AbilityAction(AIActionManager _actionManager, AbilityController _abilityController) : base(_actionManager)
        {
            AbilityController = _abilityController;
            currentDelayTime = 0.0f;
            abilityStarted = false;
        }

        public override void Update()
        {
            if (AbilityController == null)
            {
                Debug.LogError("[AbilityAction] No ability controller assigned to ability action!");
                return;
            }

            if (!abilityStarted && AbilityController.startDelay > 0.0f)
            {
                if (currentDelayTime < AbilityController.startDelay)
                {
                    currentDelayTime += Time.deltaTime;
                    return;
                }

                AbilityController.OnStart();
                abilityStarted = true;
            }

            AbilityController.OnUpdate();
        }

        public override void OnActionStart()
        {
            AbilityController.OnPreStart();
        }

        public override void OnActionFinished()
        {
            AbilityController.OnFinish();
            abilityStarted = false;
            currentDelayTime = 0.0f;
        }

        public override bool HasFinished()
        {
            return AbilityController.HasFinished();
        }
    }
}
