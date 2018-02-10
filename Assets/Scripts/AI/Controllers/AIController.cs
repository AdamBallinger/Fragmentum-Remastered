using UnityEngine;

namespace Scripts.AI.Controllers
{
    public abstract class AIController : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;

        protected AIActionManager actionManager;

        protected Animator Animator { get; private set; }

        private void Awake()
        {
            actionManager = new AIActionManager(this);
            transform = GetComponent<Transform>();
            Animator = GetComponent<Animator>();

            if(Animator == null)
            {
                Debug.LogError($"[AIController] Could not find animator component for gameobject: {gameObject.name}. Make sure it" +
                                 $"is on the same object as the AIController component.");
            }
        }
        
        private void Update()
        {
            ControllerUpdate();
            actionManager?.Update();
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
