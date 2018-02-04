using UnityEngine;
using Scripts.Abilities.Controllers;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {
        private AbilityController abilityController;

        private float currentDelayTime;

        private bool abilityInitialized, abilityStarted;

        public AbilityAction(AIActionManager _actionManager, AbilityController _abilityController) : base(_actionManager)
        {
            abilityController = _abilityController;
            currentDelayTime = 0.0f;
            abilityInitialized = false;
            abilityStarted = false;
        }

        public override void Update()
        {
            if (abilityController == null)
            {
                Debug.LogError("No ability controller assigned to ability action!");
                return;
            }

            if(!abilityInitialized)
            {
                abilityController.OnInitialize();
                abilityInitialized = true;
            }

            if (abilityController.startDelay > 0.0f)
            {
                if (currentDelayTime < abilityController.startDelay)
                {
                    currentDelayTime += Time.deltaTime;
                    return;
                }

                if (!abilityStarted)
                {
                    abilityController.OnStart();
                    abilityStarted = true;
                }
            }

            abilityController.AbilityUpdate();
            finished = abilityController.HasFinished();

            if(finished)
            {
                abilityController.OnFinish();
            }
        }
    }
}
