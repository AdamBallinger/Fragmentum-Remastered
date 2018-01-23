using System.Collections.Generic;
using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public class AIActionManager
    {
        public AIController controller;

        private AIAction defaultAction;
        private AIAction currentAction;

        private List<AIAction> queuedActions;

        public AIActionManager(AIController _controller)
        {
            controller = _controller;
            queuedActions = new List<AIAction>();
        }

        /// <summary>
        /// Sets the default action for this manager.
        /// </summary>
        /// <param name="_defaultAction"></param>
        public void SetDefaultAIAction(AIAction _defaultAction)
        {
            defaultAction = _defaultAction;
        }

        /// <summary>
        /// Enque a new AIAction to the manager.
        /// </summary>
        /// <param name="_newAction"></param>
        public void EnqueAction(AIAction _newAction)
        {
            queuedActions.Add(_newAction);
        }

        /// <summary>
        /// Immediately sets the current action for the manager, interrupting any current actions and ignoring the 
        /// action queue.
        /// </summary>
        /// <param name="_newAction"></param>
        public void SetActionImmediate(AIAction _newAction)
        {
            currentAction?.OnInterrupted();
            currentAction = _newAction;
        }

        /// <summary>
        /// Callback for when an action for this manager has finished what it needed to do.
        /// </summary>
        private void OnActionFinished()
        {
            controller.OnManagerActionFinished(currentAction);
            currentAction = null;
        }

        public bool HasQueuedActions()
        {
            return queuedActions.Count > 0;
        }

        public AIAction GetCurrentAction()
        {
            return currentAction;
        }

        public AIAction GetDefaultAction()
        {
            return defaultAction;
        }

        /// <summary>
        /// Update the managers current action and handle the action queue if a new action is needed.
        /// </summary>
        public void Update()
        {
            if (currentAction != null && currentAction.HasFinished())
            {
                OnActionFinished();
            }

            if (currentAction == null)
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
