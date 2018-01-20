using Scripts.AI.Controllers;
using UnityEngine;

namespace Scripts.AI
{
    public abstract class AIAction
    {
        protected AIBrain brain;

        protected AIAction(AIBrain _brain)
        {
            brain = _brain;
        }

        public abstract void Update(BaseAIController _controller);

        public void OnInterrupted() { }
    }
}
