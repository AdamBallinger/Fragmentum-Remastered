using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public abstract class AIAction
    {
        /// <summary>
        /// References the action manager that controls this action.
        /// </summary>
        protected AIActionManager actionManager;

        protected bool finished;

        protected AIAction(AIActionManager _actionManager)
        {
            actionManager = _actionManager;
            finished = false;
        }

        public abstract void Update();

        public virtual void OnInterrupted() { }

        public bool HasFinished()
        {
            return finished;
        }
    }
}
