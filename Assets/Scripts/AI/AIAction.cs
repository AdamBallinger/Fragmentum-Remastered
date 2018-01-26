namespace Scripts.AI
{
    public abstract class AIAction
    {
        /// <summary>
        /// References the action manager that controls this action.
        /// </summary>
        public AIActionManager ActionManager { get; }

        protected bool finished;

        protected AIAction(AIActionManager _actionManager)
        {
            ActionManager = _actionManager;
            finished = false;
        }

        public virtual void OnActionStart() { }

        public abstract void Update();

        public bool HasFinished()
        {
            return finished;
        }
    }
}
