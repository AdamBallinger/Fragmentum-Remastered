using TMPro;
using UnityEngine;

namespace Scripts.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        public DialogueType Type { get; private set; }

        [SerializeField]
        private TextMeshProUGUI dialogueText = null;

        private Dialogue currentDialogue;

        private int currentPage;

        public void SetDialogue(Dialogue _dialogue, DialogueType _type)
        {
            currentDialogue = _dialogue;
            Type = _type;
            currentPage = 0;

            dialogueText.text = currentDialogue.GetPage(currentPage);
        }

        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(currentPage >= currentDialogue.GetPageCount() - 1)
                {
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
