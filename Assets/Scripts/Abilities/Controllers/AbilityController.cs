﻿using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public abstract class AbilityController : MonoBehaviour
    {
        protected Animator Animator { get; private set; }

        [SerializeField]
        protected bool gizmosEnabled = true;

        [Tooltip("Delay in seconds before the ability starts work. This does not affect the pre start work for the ability.")]
        public float startDelay = 0.0f;

        private void Awake()
        {
            Animator = GetComponentInParent<Animator>();
        }

        /// <summary>
        /// Called as soon as the ability action for this controller is created and ignores start delay.
        /// </summary>
        public virtual void OnPreStart() { }

        /// <summary>
        /// Called when the ability controller first starts work after the start delay has been counted.
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// Handles updating the ability controller to do all ability related processing.
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Called when the ability controller has finished.
        /// </summary>
        public abstract void OnFinish();

        /// <summary>
        /// Returns when the ability controller has finished working.
        /// </summary>
        /// <returns></returns>
        public abstract bool HasFinished();
    }
}
