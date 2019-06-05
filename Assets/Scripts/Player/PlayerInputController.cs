using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        /// <summary>
        /// Controls whether the player can use their physical input device to control the player.
        /// </summary>
        public bool UsePhysicalInput { get; set; } = true;

        public bool EnableJump { get; set; } = true;
        public bool EnableDash { get; set; } = true;
        public bool EnableBlock { get; set; } = true;
        
        /// <summary>
        /// Current left joystick / (A/D) Keys delta.
        /// </summary>
        public float HDelta { get; set; }
        
        /// <summary>
        /// Stores whether the jump key was pressed this frame.
        /// </summary>
        public bool Jump { get; set; }
        
        /// <summary>
        /// Stores whether the dash key was pressed this frame.
        /// </summary>
        public bool Dash { get; set; }
        
        /// <summary>
        /// Stores whether the block key was pressed this frame.
        /// </summary>
        public bool Block { get; set; }

        private void Update()
        {
            // Only update input states from unity input system if using physical input.
            if (UsePhysicalInput)
            {
                HDelta = 0.0f;

                Jump = false;
                Dash = false;
                Block = false;

                HDelta = Input.GetAxis("Horizontal");
                Jump = Input.GetButtonDown("Jump") && EnableJump;
                Dash = Input.GetButtonDown("Dash") && EnableDash;
                Block = Input.GetButton("Block") && EnableBlock;
            }
        }
        
        /// <summary>
        /// Toggles if the player can use a controller / keyboard for player input.
        /// </summary>
        /// <param name="_toggle"></param>
        public void SetPhysicalInput(bool _toggle)
        {
            UsePhysicalInput = _toggle;
            HDelta = 0.0f;
        }
        
        public void DisableAllActions()
        {
            EnableJump = false;
            EnableDash = false;
            EnableBlock = false;
        }
        
        public void EnableAllActions()
        {
            EnableJump = true;
            EnableDash = true;
            EnableBlock = true;
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