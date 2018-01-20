using System.Collections.Generic;
using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public class AIBrain
    {
        public BaseAIController controller;

        private AIAction defaultAction;
        private AIAction currentAction;

        private List<AIAction> queuedActions;

        public AIBrain(BaseAIController _controller)
        {
            controller = _controller;
            queuedActions = new List<AIAction>();
        }

        /// <summary>
        /// Sets the default action for this AI brain.
        /// </summary>
        /// <param name="_defaultAction"></param>
        public void SetDefaultAIAction(AIAction _defaultAction)
        {
            defaultAction = _defaultAction;
        }

        /// <summary>
        /// Enque a new AIAction to the brain.
        /// </summary>
        /// <param name="_newAction"></param>
        public void EnqueAction(AIAction _newAction)
        {
            queuedActions.Add(_newAction);
        }

        /// <summary>
        /// Immediately sets the current action for the brain, interrupting any current actions and ignoring the 
        /// action queue.
        /// </summary>
        /// <param name="_newAction"></param>
        public void SetActionImmediate(AIAction _newAction)
        {
            currentAction?.OnInterrupted();
            currentAction = _newAction;
        }

        /// <summary>
        /// Callback for when an action for this brain has finished what it needed to do.
        /// </summary>
        public void OnActionFinished()
        {
            controller.OnBrainActionFinished(currentAction);
            currentAction = null;
        }

        /// <summary>
        /// Update the brains current action and handle switching to the next queued actions if available.
        /// </summary>
        public void Update()
        {
            if(currentAction == null)
            {
                if(queuedActions.Count > 0)
                {
                    currentAction = queuedActions[0];
                    queuedActions.RemoveAt(0);
                }
                else
                {
                    currentAction = defaultAction;
                }
            }

            currentAction?.Update(controller);
        }
    }
}
