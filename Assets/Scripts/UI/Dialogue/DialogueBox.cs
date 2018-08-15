using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        public DialogueType Type { get; private set; }

        [SerializeField]
        private TextMeshProUGUI dialogueText = null;

        private Dialogue currentDialogue;

        private int currentPage;

        private UnityEvent onCloseCallback;

        public void SetDialogue(Dialogue _dialogue, DialogueType _type, UnityEvent _onCloseCallback)
        {
            currentDialogue = _dialogue;
            Type = _type;
            currentPage = 0;
            onCloseCallback = _onCloseCallback;

            dialogueText.text = currentDialogue.GetPage(currentPage);
        }

        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(currentPage >= currentDialogue.GetPageCount() - 1)
                {
                    onCloseCallback?.Invoke();
                    FindObjectOfType<DialogueController>().HideDialogue(Type);
                }
                else
                {
                    currentPage++;
                    dialogueText.text = currentDialogue.GetPage(currentPage);
                }
            }
        }
    }
}
