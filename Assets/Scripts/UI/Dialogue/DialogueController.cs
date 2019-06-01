using Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField]
        private DialogueBox chatDialogueBox = null;

        private PlayerController playerController;

        public void ShowDialogue(Dialogue _dialogue, UnityEvent _onCloseCallback)
        {
            var db = ToggleDialogueBox(true);
            db.SetDialogue(_dialogue, _onCloseCallback);
        }

        public void HideDialogue()
        {
            ToggleDialogueBox(false);
        }

        private DialogueBox ToggleDialogueBox(bool _state)
        {
            if(playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }

            chatDialogueBox.gameObject.SetActive(_state);
            //playerController.ControlsEnabled = !_state;
            return chatDialogueBox;
        }
    }
}
