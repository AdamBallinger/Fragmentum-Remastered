using TMPro;
using UnityEngine;

namespace Scripts.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogueText = null;

        private Dialogue currentDialogue;

        private int currentPage;

        public void SetDialogue(Dialogue _dialogue)
        {
            currentDialogue = _dialogue;
            currentPage = 0;

            dialogueText.text = currentDialogue.GetPage(currentPage);
        }

        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(currentPage >= currentDialogue.GetPageCount() - 1)
                {
                    FindObjectOfType<DialogueController>().HideDialogue(this);
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
