using Scripts.Player;
using UnityEngine;

namespace Scripts.UI.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField]
        private DialogueBox chatDialogueBox = null;

        [SerializeField]
        private DialogueBox hintDialogueBox = null;

        private PlayerController playerController;

        public void ShowDialogue(DialogueType _type, Dialogue _dialogue)
        {
            var db = ToggleDialogueBox(_type, true);
            db.SetDialogue(_dialogue, _type);
        }

        public void HideDialogue(DialogueType _type)
        {
            ToggleDialogueBox(_type, false);
        }

        private DialogueBox ToggleDialogueBox(DialogueType _type, bool _state)
        {
            if(playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }

            switch (_type)
            {
                case DialogueType.Chat:
                    chatDialogueBox.gameObject.SetActive(_state);
                    playerController.ControlsEnabled = !_state;
                    return chatDialogueBox;

                case DialogueType.UI_Hint:
                    hintDialogueBox.gameObject.SetActive(_state);
                    return hintDialogueBox;
            }

            return null;
        }
    }
}
