﻿using UnityEngine;

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
            if (ActionManager.Controller.usesGravity)
            {
                // Move with 0 force so that gravity still gets applied in they are for some reason idle in the air.
                ActionManager.Controller.Controller.SimpleMove(Vector3.zero);
            }

            if (duration <= 0.0f)
            {
                return;
            }

            currentIdleTime += Time.deltaTime;
        }

        public override void OnActionFinished()
        {
            currentIdleTime = 0.0f;
        }

        public override bool HasFinished()
        {
            return currentIdleTime >= duration;
        }
    }
}
