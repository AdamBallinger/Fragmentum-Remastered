using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        public bool UsePhysicalInput { get; set; } = true;

        public float HDelta { get; set; }
        public bool Jump { get; set; }
        public bool Dash { get; set; }
        public bool Block { get; set; }

        public void Update()
        {
            if (UsePhysicalInput)
            {
                HDelta = 0.0f;

                Jump = false;
                Dash = false;
                Block = false;

                HDelta = Input.GetAxis("Horizontal");
                Jump = Input.GetButtonDown("Jump");
                Dash = Input.GetButtonDown("Dash");
                Block = Input.GetButton("Block");
            }
        }
        
        public void SetPhysicalInput(bool _toggle)
        {
            UsePhysicalInput = _toggle;
        }

        /// <summary>
        /// Moves the player using virtual joystick values. -1 Moves the player left, 0 stops the player,  1 moves the player right.
        /// </summary>
        public void VirtualMovePlayer(float _virtualInput)
        {
            // Block virtual input if the controller is set to use physical input.
            if (UsePhysicalInput)
            {
                return;
            }

            HDelta = Mathf.Clamp(_virtualInput, -1.0f, 1.0f);
        }
    }
}