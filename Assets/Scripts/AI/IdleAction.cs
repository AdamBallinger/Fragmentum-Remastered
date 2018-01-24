using Scripts.AI.Controllers;
using UnityEngine;

namespace Scripts.AI
{
    public class IdleAction : AIAction
    {
        /// <summary>
        /// Duration in seconds to remain idle for.
        /// </summary>
        private float duration;

        private float currentIdleTime;

        /// <summary>
        /// Create a new idle state with a given duration in seconds to remain idle for. If a duratiin of 0 is given
        /// then the AI will remain idle until manually interrupted.
        /// </summary>
        /// <param name="_actionManager"></param>
        /// <param name="_duration"></param>
        public IdleAction(AIActionManager _actionManager, float _duration) : base(_actionManager)
        {
            duration = _duration;
            currentIdleTime = 0.0f;
        }

        public override void Update()
        {
            if(duration <= 0.0f)
            {
                return;
            }

            currentIdleTime += Time.deltaTime;

            if(currentIdleTime >= duration)
            {
                finished = true;
            }
        }
    }
}
