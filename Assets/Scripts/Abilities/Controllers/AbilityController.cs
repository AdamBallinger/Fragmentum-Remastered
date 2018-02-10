using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public abstract class AbilityController : MonoBehaviour
    {
        protected Animator Animator { get; private set; }

        [SerializeField]
        protected bool gizmosEnabled = true;

        [Tooltip("Delay in seconds before the ability starts work.")]
        public float startDelay = 0.0f;

        /// <summary>
        /// Determines if this ability controller will override the AI controllers rotation commands.
        /// </summary>
        public bool OverrideRotation { get; protected set; } = false;

        private void Awake()
        {
            Animator = GetComponentInParent<Animator>();
        }

        /// <summary>
        /// Called as soon as the ability action for this controller is created and ignores start delay.
        /// </summary>
        public abstract void OnInitialize();

        /// <summary>
        /// Called when the ability controller first starts work after the start delay has been counted.
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// Handles updating the ability controller to do all ability related processing.
        /// </summary>
        public abstract void AbilityUpdate();

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
