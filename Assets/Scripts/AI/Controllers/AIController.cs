using UnityEngine;

namespace Scripts.AI.Controllers
{
    public abstract class AIController : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;

        protected AIActionManager actionManager;

        private void Awake()
        {
            actionManager = new AIActionManager(this);
            transform = GetComponent<Transform>();
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
        /// Callback for when the action manager finished its last action.
        /// </summary>
        public abstract void OnManagerActionFinished(AIAction _action);
    }
}
