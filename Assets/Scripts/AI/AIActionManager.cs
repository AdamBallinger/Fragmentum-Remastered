using System.Collections.Generic;
using UnityEngine;
using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public class AIActionManager
    {
        public AIController controller;

        private Dictionary<int, List<AIAction>> channels;
        private Dictionary<int, AIAction> channelActions;
        private Dictionary<int, AIAction> channelDefaultActions;

        private const int MAX_CHANNELS = 2;

        public AIActionManager(AIController _controller)
        {
            controller = _controller;
            channels = new Dictionary<int, List<AIAction>>();
            channelActions = new Dictionary<int, AIAction>();
            channelDefaultActions = new Dictionary<int, AIAction>();

            for(var i = 1; i <= MAX_CHANNELS; i++)
            {
                channels.Add(i, new List<AIAction>());
                channelActions.Add(i, null);
                channelDefaultActions.Add(i, null);
            }
        }

        /// <summary>
        /// Sets the default action for this manager.
        /// </summary>
        /// <param name="_defaultAction"></param>
        /// <param name="_channel"></param>
        public void SetDefaultAIAction(AIAction _defaultAction, int _channel = 1)
        {
            if(!ChannelValid(_channel))
            {
                return;
            }

            channelDefaultActions[_channel] = _defaultAction;
        }

        /// <summary>
        /// Enque a new AIAction to the manager.
        /// </summary>
        /// <param name="_newAction"></param>
        /// <param name="_channel"></param>
        public void EnqueAction(AIAction _newAction, int _channel = 1)
        {
            if (!ChannelValid(_channel))
            {
                return;
            }

            channels[_channel].Add(_newAction);
        }

        /// <summary>
        /// Immediately sets the current action for the manager, interrupting any current actions and ignoring the 
        /// action queue.
        /// </summary>
        /// <param name="_newAction"></param>
        /// <param name="_channel"></param>
        public void SetActionImmediate(AIAction _newAction, int _channel = 1)
        {
            if (!ChannelValid(_channel))
            {
                return;
            }

            channelActions[_channel]?.OnInterrupted();
            channelActions[_channel] = _newAction;
        }

        /// <summary>
        /// Callback for when an action for this manager has finished what it needed to do.
        /// </summary>
        private void OnActionFinished(int _channel)
        {
            controller.OnManagerActionFinished(channelActions[_channel]);
            channelActions[_channel] = null;
        }

        public bool HasQueuedActions(int _channel = 1)
        {
            if (!ChannelValid(_channel))
            {
                return false;
            }

            return channels[_channel].Count > 0;
        }

        public AIAction GetCurrentAction(int _channel = 1)
        {
            return !ChannelValid(_channel) ? null : channelActions[_channel];
        }

        public AIAction GetDefaultAction(int _channel = 1)
        {
            return !ChannelValid(_channel) ? null : channelDefaultActions[_channel];
        }

        /// <summary>
        /// Update the managers current action and handle the action queue if a new action is needed.
        /// </summary>
        public void Update()
        {
            foreach(var channel in channels)
            {
                if(channelActions[channel.Key] != null && channelActions[channel.Key].HasFinished())
                {
                    OnActionFinished(channel.Key);
                }

                if(channelActions[channel.Key] == null)
                {
                    if(channel.Value.Count > 0)
                    {
                        channelActions[channel.Key] = channel.Value[0];
                        channel.Value.RemoveAt(0);
                    }
                    else
                    {
                        channelActions[channel.Key] = channelDefaultActions[channel.Key];
                    }
                }

                channelActions[channel.Key]?.Update(controller);
            }
        }

        private bool ChannelValid(int _channel)
        {
            if(_channel >= 1 && _channel <= MAX_CHANNELS)
            {
                return true;
            }
            
            Debug.LogError($"You can only use channels: 1 to {MAX_CHANNELS} with the action manager!");
            return false;
        }
    }
}
