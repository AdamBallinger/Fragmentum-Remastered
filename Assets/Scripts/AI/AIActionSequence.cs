using System;
using System.Collections.Generic;

namespace Scripts.AI
{
    public class AIActionSequence
    {
        private List<AIAction> Sequence { get; }

        private AIAction ActiveAtion { get; set; }

        /// <summary>
        /// Callback for when the sequence finishes or completes a cycle if set to repeat.
        /// </summary>
        private Action completeCallback;

        private int currentSequenceIndex;

        private bool repeating;

        public AIActionSequence(Action _sequenceCompleteCallback) : this(_sequenceCompleteCallback, false) { }

        public AIActionSequence(bool _repeating) : this(null, _repeating) { }

        public AIActionSequence(Action _sequenceCompleteCallback = null, bool _repeating = false)
        {
            Sequence = new List<AIAction>();
            currentSequenceIndex = -1;
            repeating = _repeating;
            completeCallback = _sequenceCompleteCallback;
        }

        /// <summary>
        /// Returns the currently active action in the sequence.
        /// </summary>
        /// <returns></returns>
        public AIAction GetActiveAction()
        {
            if(ActiveAtion == null || ActiveAtion.HasFinished())
            {
                ActiveAtion?.OnActionFinished();
                ActiveAtion = GetNextInSequence();
                ActiveAtion?.OnActionStart();
            }

            return ActiveAtion;
        }

        /// <summary>
        /// Returns the next action in the sequence. If the sequence is repeating, it will automatically wrap around
        /// if the sequence was on its last action. Null is returned if the sequence was already finished.
        /// </summary>
        /// <returns></returns>
        private AIAction GetNextInSequence()
        {
            currentSequenceIndex++;

            if (currentSequenceIndex >= Sequence.Count)
            {
                if(repeating)
                {
                    currentSequenceIndex = 0;
                }
                else
                {
                    return null;
                }
            }

            return Sequence[currentSequenceIndex];
        }

        public void ForceFinishCurrentAction()
        {
            if(ActiveAtion != null)
            {
                ActiveAtion?.OnActionFinished();
                ActiveAtion = GetNextInSequence();
            }
        }

        /// <summary>
        /// Returns if the sequence has finished. If the sequence is repeating this will always be false.
        /// </summary>
        /// <returns></returns>
        public bool SequenceFinished()
        {
            if(!repeating && currentSequenceIndex >= Sequence.Count)
            {
                completeCallback?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Add a single action to the end of the sequence.
        /// </summary>
        /// <param name="_action"></param>
        public void AddActionToSequence(AIAction _action)
        {
            Sequence.Add(_action);
        }

        /// <summary>
        /// Add a range of actions onto the end of the sequence.
        /// </summary>
        /// <param name="_actions"></param>
        public void AddActionsToSequence(params AIAction[] _actions)
        {
            Sequence.AddRange(_actions);
        }

        /// <summary>
        /// Clear all actions from the sequence.
        /// </summary>
        public void ClearSequence()
        {
            Sequence.Clear();
        }
    }

    public enum SequenceStopBehaviour
    {
        ForceFinish,
        Pause
    }
}
