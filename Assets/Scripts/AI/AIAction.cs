using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public abstract class AIAction
    {
        protected AIActionManager actionManager;

        protected bool finished;

        protected AIAction(AIActionManager _actionManager)
        {
            actionManager = _actionManager;
            finished = false;
        }

        public abstract void Update(AIController _controller);

        public void OnInterrupted() { }

        public bool HasFinished()
        {
            return finished;
        }
    }
}
