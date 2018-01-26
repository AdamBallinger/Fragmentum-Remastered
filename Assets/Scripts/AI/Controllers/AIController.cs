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
        }
        
        private void Update()
        {
            actionManager?.Update();
            ControllerUpdate();
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
