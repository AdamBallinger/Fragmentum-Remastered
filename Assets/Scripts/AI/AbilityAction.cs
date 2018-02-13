using UnityEngine;
using Scripts.Abilities.Controllers;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {
        public AbilityController AbilityController { get; }

        private float currentDelayTime;

        private bool abilityInitialized, abilityStarted;

        public AbilityAction(AIActionManager _actionManager, AbilityController _abilityController) : base(_actionManager)
        {
            AbilityController = _abilityController;
            currentDelayTime = 0.0f;
            abilityInitialized = false;
            abilityStarted = false;
        }

        public override void Update()
        {
            if (AbilityController == null)
            {
                Debug.LogError("[AbilityAction] No ability controller assigned to ability action!");
                return;
            }

            if(!abilityInitialized)
            {
                AbilityController.OnPreStart();
                abilityInitialized = true;
            }

            if (AbilityController.startDelay > 0.0f)
            {
                if (currentDelayTime < AbilityController.startDelay)
                {
                    currentDelayTime += Time.deltaTime;
                    return;
                }
            }

            if (!abilityStarted)
            {
                AbilityController.OnStart();
                abilityStarted = true;
            }

            AbilityController.OnUpdate();
            finished = AbilityController.HasFinished();

            if(finished)
            {
                AbilityController.OnFinish();
            }
        }
    }
}
