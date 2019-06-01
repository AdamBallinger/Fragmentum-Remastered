using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueText = null;

        private Dialogue currentDialogue;

        private int currentPage;

        private UnityEvent onCloseCallback;

        public void SetDialogue(Dialogue _dialogue, UnityEvent _onCloseCallback)
        {
            currentDialogue = _dialogue;
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
                    FindObjectOfType<DialogueController>().HideDialogue();
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
