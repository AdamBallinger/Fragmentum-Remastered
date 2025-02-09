﻿namespace Scripts.AI
{
    public abstract class AIAction
    {
        /// <summary>
        /// References the action manager that controls this action.
        /// </summary>
        public AIActionManager ActionManager { get; }

        protected AIAction(AIActionManager _actionManager)
        {
            ActionManager = _actionManager;
        }

        public virtual void OnActionStart() { }

        public abstract void Update();

        public virtual void OnActionFinished() { }

        public abstract bool HasFinished();
    }
}
