using UnityEngine;
using Scripts.Abilities.Controllers;

namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {
        private AbilityController abilityController;

        private float currentDelayTime;

        public AbilityAction(AIActionManager _actionManager, AbilityController _abilityController) : base(_actionManager)
        {
            abilityController = _abilityController;
            currentDelayTime = 0.0f;
        }

        public override void Update()
        {
            if(abilityController == null)
            {
                Debug.LogError("No ability controller assigned to ability action!");
                return;
            }

            if(abilityController.AbilityData.startDelay > 0.0f)
            {
                if(currentDelayTime >= abilityController.AbilityData.startDelay)
                {
                    finished = abilityController.Update(this);
                }
                else
                {
                    currentDelayTime += Time.deltaTime;
                }
            }
            else
            {
                finished = abilityController.Update(this);
            }
        }
    }
}
