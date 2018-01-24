using System.Collections.Generic;
using UnityEngine;
using Scripts.AI.Controllers;

namespace Scripts.AI
{
    public class AIActionManager
    {
        /// <summary>
        /// Reference to the AI Controller this action manager is assigned too.
        /// </summary>
        public AIController Controller { get; }

        /// <summary>
        /// Stores the action queue for each channel.
        /// </summary>
        private Dictionary<int, List<AIAction>> channels;

        /// <summary>
        /// Stores the current action being updated for each channel.
        /// </summary>
        private Dictionary<int, AIAction> channelCurrentAction;

        /// <summary>
        /// Stores the current default action for each channel.
        /// </summary>
        private Dictionary<int, AIAction> channelDefaultActions;

        /// <summary>
        /// Defines the maximum number of channels the action manager can use.
        /// </summary>
        private const int MAX_CHANNELS = 2;

        /// <summary>
        /// Creates an action manager for a given AI Controller.
        /// </summary>
        /// <param name="_controller"></param>
        public AIActionManager(AIController _controller)
        {
            Controller = _controller;
            channels = new Dictionary<int, List<AIAction>>();
            channelCurrentAction = new Dictionary<int, AIAction>();
            channelDefaultActions = new Dictionary<int, AIAction>();

            for(var i = 1; i <= MAX_CHANNELS; i++)
            {
                channels.Add(i, new List<AIAction>());
                channelCurrentAction.Add(i, null);
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

            channelCurrentAction[_channel]?.OnInterrupted();
            channelCurrentAction[_channel] = _newAction;
        }

        private void OnActionStart(int _channel)
        {
            Controller.OnManagerActionStart(channelCurrentAction[_channel]);
        }

        /// <summary>
        /// Callback for when an action for this manager has finished what it needed to do.
        /// </summary>
        private void OnActionFinished(int _channel)
        {
            Controller.OnManagerActionFinished(channelCurrentAction[_channel]);
            channelCurrentAction[_channel] = null;
        }

        /// <summary>
        /// Returns whether a given channel has any queued actions.
        /// </summary>
        /// <param name="_channel"></param>
        /// <returns></returns>
        public bool HasQueuedActions(int _channel = 1)
        {
            if (!ChannelValid(_channel))
            {
                return false;
            }

            return channels[_channel].Count > 0;
        }

        /// <summary>
        /// Gets the current action for a given channel.
        /// </summary>
        /// <param name="_channel"></param>
        /// <returns></returns>
        public AIAction GetCurrentAction(int _channel = 1)
        {
            return !ChannelValid(_channel) ? null : channelCurrentAction[_channel];
        }

        /// <summary>
        /// Gets the default action for a given channel.
        /// </summary>
        /// <param name="_channel"></param>
        /// <returns></returns>
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
                if(channelCurrentAction[channel.Key] != null && channelCurrentAction[channel.Key].HasFinished())
                {
                    OnActionFinished(channel.Key);
                }

                if(channelCurrentAction[channel.Key] == null)
                {
                    if(channel.Value.Count > 0)
                    {
                        channelCurrentAction[channel.Key] = channel.Value[0];
                        channel.Value.RemoveAt(0);
                        OnActionStart(channel.Key);
                    }
                    else
                    {
                        channelCurrentAction[channel.Key] = channelDefaultActions[channel.Key];
                    }
                }

                channelCurrentAction[channel.Key]?.Update();
            }
        }

        /// <summary>
        /// Validates whether a channel is valid.
        /// </summary>
        /// <param name="_channel"></param>
        /// <returns></returns>
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
