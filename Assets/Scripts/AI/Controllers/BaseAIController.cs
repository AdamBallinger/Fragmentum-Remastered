using UnityEngine;

namespace Scripts.AI.Controllers
{
    public abstract class BaseAIController : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;

        protected AIBrain brain;

        private void Awake()
        {
            brain = new AIBrain(this);
            transform = GetComponent<Transform>();
        }

        private void Update()
        {
            brain?.Update();
            ControllerUpdate();
        }

        /// <summary>
        /// Handles custom controller update logic.
        /// </summary>
        protected abstract void ControllerUpdate();

        /// <summary>
        /// Callback for when the brain finished an action. Passes in the action that was finished.
        /// </summary>
        public abstract void OnBrainActionFinished(AIAction _action);
    }
}
