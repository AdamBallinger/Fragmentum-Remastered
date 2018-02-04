using UnityEngine;
using Scripts.Abilities.Controllers;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {
        private AbilityController abilityController;

        private float currentDelayTime;

        private bool abilityStarted = false;

        public AbilityAction(AIActionManager _actionManager, AbilityController _abilityController) : base(_actionManager)
        {
            abilityController = _abilityController;
            currentDelayTime = 0.0f;
            abilityController.OnInitialize();
        }

        public override void Update()
        {
            if (abilityController == null)
            {
                Debug.LogError("No ability controller assigned to ability action!");
                return;
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
