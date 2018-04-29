using UnityEngine;

namespace Scripts.UI.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        public DialogueType type = DialogueType.Chat;

        [Tooltip("Toggles if this dialogue trigger can be triggered multiple times.")]
        public bool canTriggerMultiple;

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

                controller.ShowDialogue(type, dialogue);
            }
        }
    }
}
