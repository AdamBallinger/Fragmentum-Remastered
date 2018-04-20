using System;
using System.Collections.Generic;
using Scripts.AI.Controllers;
using UnityEngine;

namespace Scripts.AI
{
    public class AIActionManager
    {
        /// <summary>
        /// Reference to the AI Controller this action manager is assigned too.
        /// </summary>
        public AIController Controller { get; }

        /// <summary>
        /// The action queue for this action manager.
        /// </summary>
        private List<AIAction> actionQueue;

        /// <summary>
        /// Default action of this action manager.
        /// </summary>
        private AIAction defaultAction;

        /// <summary>
        /// Current action for this manager that is being updated.
        /// </summary>
        private AIAction currentAction;

        /// <summary>
        /// The current AI action sequence the manager is processing.
        /// </summary>
        private AIActionSequence currentSequence;

        /// <summary>
        /// Is the current sequence (if any) paused?
        /// </summary>
        private bool sequencePaused;

        /// <summary>
        /// Creates an action manager for a given AI Controller.
        /// </summary>
        /// <param name="_controller"></param>
        public AIActionManager(AIController _controller)
        {
            actionQueue = new List<AIAction>();

            Controller = _controller;
        }

        /// <summary>
        /// Returns the current sequence for this manager.
        /// </summary>
        /// <returns></returns>
        public AIActionSequence GetSequence()
        {
            return currentSequence;
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
        /// Enques an action to the manager.
        /// </summary>
        /// <param name="_newAction"></param>
        public void EnqueAction(AIAction _newAction)
        {
            actionQueue.Add(_newAction);
        }

        /// <summary>
        /// Immediately sets the current action for the manager, interrupting any current actions and ignoring the 
        /// action queue.
        /// </summary>
        /// <param name="_newAction"></param>
        public void SetActionImmediate(AIAction _newAction)
        {
            OnActionFinished();

            currentAction = _newAction;
        }

        /// <summary>
        /// Sets the action sequence for the manager to process.
        /// </summary>
        /// <param name="_sequence"></param>
        public void SetActionSequence(AIActionSequence _sequence)
        {
            currentSequence = _sequence;
        }

        /// <summary>
        /// Event for when a new action starts for this manager.
        /// </summary>
        private void OnActionStart()
        {
            if(currentAction == null)
            {
                return;
            }

            currentAction.OnActionStart();
            Controller.OnManagerActionStart(currentAction);
        }

        /// <summary>
        /// Event for when an action for this manager has finished what it needed to do.
        /// </summary>
        private void OnActionFinished()
        {
            if(currentAction == null)
            {
                return;
            }

            Controller.OnManagerActionFinished(currentAction);
            currentAction.OnActionFinished();
            currentAction = null;
        }

        /// <summary>
        /// Returns whether the manager has any queued actions.
        /// </summary>
        /// <returns></returns>
        public bool HasQueuedActions()
        {
            return actionQueue.Count > 0;
        }

        /// <summary>
        /// Gets the current action for this manager.
        /// </summary>
        /// <returns></returns>
        public AIAction GetCurrentAction()
        {
            return currentAction;
        }

        /// <summary>
        /// Gets the default action for this manager.
        /// </summary>
        /// <returns></returns>
        public AIAction GetDefaultAction()
        {
            return defaultAction;
        }

        /// <summary>
        /// Update the managers current action and handle the action queue if a new action is needed.
        /// </summary>
        public void Update()
        {
            if(currentAction != null && currentAction.HasFinished())
            {
                OnActionFinished();
            }

            if(currentAction == null)
            {
                if(actionQueue.Count > 0)
                {
                    currentAction = actionQueue[0];
                    actionQueue.RemoveAt(0);
                    OnActionStart();
                }
                else
                {
                    currentAction = defaultAction;
                    OnActionStart();
                }
            }

            currentAction?.Update();

            if (!sequencePaused)
            {
                currentSequence?.GetActiveAction()?.Update();
            }
        }

        public void ResumeSequence()
        {
            sequencePaused = false;
        }

        public void StopSequence(SequenceStopBehaviour _behaviour = SequenceStopBehaviour.Pause)
        {
            sequencePaused = true;

            if(_behaviour == SequenceStopBehaviour.ForceFinish)
            {
                currentSequence?.ForceFinishCurrentAction();
            }
        }
    }
}
