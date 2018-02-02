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

        private void Awake()
        {
            Animator = GetComponentInParent<Animator>();
        }

        /// <summary>
        /// Called when the ability controller first starts work.
        /// </summary>
        public abstract void OnAbilityStart();

        /// <summary>
        /// Handles updating the ability controller to do all ability related processing.
        /// </summary>
        public abstract void AbilityUpdate();

        /// <summary>
        /// Called when the ability controller has finished.
        /// </summary>
        public abstract void OnAbilityFinished();

        /// <summary>
        /// Returns when the ability controller has finished working.
        /// </summary>
        /// <returns></returns>
        public abstract bool HasFinished();
    }
}
