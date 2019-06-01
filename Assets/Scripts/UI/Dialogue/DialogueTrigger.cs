using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        [Tooltip("Toggles if this dialogue trigger can be triggered multiple times.")]
        public bool canTriggerMultiple;

        public UnityEvent onDialogueOpen;
        public UnityEvent onDialogueClose;      

        private DialogueController controller;

        private bool hasTriggered;

        private void Start()
        {
            dialogue.Create();

            controller = FindObjectOfType<DialogueController>();
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (controller == null)
            {
                Debug.LogError("[DialogueTrigger] -> Failed to find instance of DialogueController in scene!");
                return;
            }

            if (_collider.gameObject.CompareTag("Player") && !hasTriggered)
            {
                if(!canTriggerMultiple)
                {
                    hasTriggered = true;
                }

                onDialogueOpen?.Invoke();
                controller.ShowDialogue(dialogue, onDialogueClose);
            }
        }
    }
}
