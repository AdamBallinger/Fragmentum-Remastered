using UnityEngine;

namespace Scripts.UI.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField]
        private DialogueBox chatDialogueBox = null;

        [SerializeField]
        private DialogueBox hintDialogueBox = null;

        public void ShowDialogue(DialogueType _type, Dialogue _dialogue)
        {
            var db = ToggleDialogueBox(_type, true);
            db.SetDialogue(_dialogue);
        }

        public void HideDialogue(DialogueBox _dialogueBox)
        {
            _dialogueBox.gameObject.SetActive(false);
        }

        private DialogueBox ToggleDialogueBox(DialogueType _type, bool _state)
        {
            switch (_type)
            {
                case DialogueType.Chat:
                    chatDialogueBox.gameObject.SetActive(_state);
                    return chatDialogueBox;

                case DialogueType.UI_Hint:
                    hintDialogueBox.gameObject.SetActive(_state);
                    return hintDialogueBox;
            }

            return null;
        }
    }
}
