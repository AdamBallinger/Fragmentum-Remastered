using Scripts.Utils;
using UnityEngine;

namespace Scripts.AI.Controllers
{
    public abstract class AIController : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;

        /// <summary>
        /// Reference to the rotator component of this AI, which handles rotating the AI body.
        /// </summary>
        public Rotator Rotator { get; private set; }

        /// <summary>
        /// Property defines if the AIController controls the rotation of the bat. Useful for allowing external components
        /// to control the rotation of the AI.
        /// </summary>
        public bool AllowRotation { get; set; }

        protected AIActionManager actionManager;

        protected Animator Animator { get; private set; }

        private void Awake()
        {
            actionManager = new AIActionManager(this);
            transform = GetComponent<Transform>();
            Animator = GetComponent<Animator>();
            Rotator = GetComponent<Rotator>();

            if(Animator == null)
            {
                Debug.LogError($"[AIController] Could not find animator component for gameobject: {gameObject.name}. Make sure it " +
                                 "is on the same object as the AIController component.");
            }

            if(Rotator == null)
            {
                Debug.LogError($"[AIController] Could not find Rotator component for gameobject: {gameObject.name}. Make sure it " +
                               "is on the same object as the AIController component.");
            }
        }
        
        private void Update()
        {
            ControllerUpdate();
            actionManager?.Update();
        }

        /// <summary>
        /// Rotates the AI towards the given target position.
        /// </summary>
        /// <param name="_target"></param>
        protected void RotateTowards(Vector3 _target)
        {
            if(!AllowRotation)
            {
                return;
            }

            Rotator.rotateTarget = _target;
        }

        /// <summary>
        /// Handles custom controller update logic.
        /// </summary>
        protected abstract void ControllerUpdate();

        /// <summary>
        /// Event for when the action manager starts a new action.
        /// </summary>
        /// <param name="_action"></param>
        public virtual void OnManagerActionStart(AIAction _action) { }

        /// <summary>
        /// Event for when the action manager finished its last action.
        /// </summary>
        public virtual void OnManagerActionFinished(AIAction _action) { }

    }
}
