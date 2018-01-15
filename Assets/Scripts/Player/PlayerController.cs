﻿using TMPro;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController Controller { get; set; }

        private Animator Animator { get; set; }

        public Vector3 Heading => _transform.forward;

        public Vector3 Velocity { get; private set; }

        public bool Dashing { get; private set; }

        public bool Blocking { get; private set; }

        public bool Grounded => Controller.isGrounded;

        public bool Falling => !Grounded && Velocity.y < 0.0f && GetDistanceToGround() >= fallDistanceThreshold;

        private float HDelta { get; set; }

        public TextMeshProUGUI debugText;

        [Header("Movement Settings")]
        public float moveSpeed = 1.0f;
        public float jumpStrength = 1.0f;
        public float dashStrength = 1.0f;
        [Range(0, 2)]
        public float dashDuration = 1.0f;
        [Range(0, 2)]
        [Tooltip("Delay in seconds between being able to dash repeatedly.")]
        public float dashDelay = 0.5f;

        [Header("Physics Settings")]
        [Range(0.0f, 10.0f)]
        public float gravityStrength = 1.0f;
        [Tooltip("The distance that the player must be from the ground before the controller thinks the player is falling.")]
        public float fallDistanceThreshold = 1.0f;

        private Vector3 dashHeading;

        private bool doubleJumping;

        // The current dash time.
        private float dashTime;

        private bool jumpPressed;
        private bool dashPressed;

        private Transform _transform;

        private void Start()
        {
            Controller = GetComponent<CharacterController>();
            Animator = GetComponentInChildren<Animator>();

            _transform = transform;
        }

        private void SetAnimations()
        {
            Animator.SetFloat("animSpeedMod", HDelta);
            Animator.SetBool("isGrounded", Grounded);
            Animator.SetBool("isRunning", HDelta != 0.0f);
            Animator.SetBool("isDashing", Dashing);
            Animator.SetBool("isFalling", Falling);
            Animator.SetBool("isBlocking", Blocking);
        }

        private void Update()
        {
            HDelta = Input.GetAxis("Horizontal");

            ProcessJumping();
            ProcessDashing();
            ProcessBlocking();
            ProcessMovement();

            ProcessGravity();

            SetAnimations();

            Controller.Move(Velocity);

            var groundObj = CastRayToGround();
            var objName = groundObj.transform != null ? groundObj.transform.name : "Nothing";
            // Update debug stuff after all movement is processed.
            debugText.text = $"V- [x:{Velocity.x:F}] y:{Velocity.y:F}, z:{Velocity.z:F}]\n" +
                        $"G- [{Grounded}] F- [{Falling}]\n" +
                        $"GD- [{groundObj.distance:F}] GO- [{objName}]\n" +
                        $"D- [{Dashing}] B- [{Blocking}]";

            // Reset X/Z velocity but preserve Y for falling etc.
            SetVelocity(0.0f, Grounded ? 0.0f : Velocity.y, 0.0f);
        }

        /// <summary>
        /// Handles all movement for the player and calculates the final velocity for the player (pre-gravity) this frame
        /// to be applied to the character controller.
        /// </summary>
        private void ProcessMovement()
        {
            if(Dashing)
            {
                Velocity = dashHeading * Time.deltaTime;
                dashTime += Time.deltaTime;

                if(dashTime >= dashDuration)
                {
                    dashTime = dashDelay;
                    Dashing = false;
                }
            }
            else
            {
                // Rotate player to face where they will move.
                if (HDelta != 0.0f)
                {
                    var directionMod = HDelta < 0.0f ? -1.0f : 1.0f;
                    _transform.rotation = Quaternion.AngleAxis(90.0f * directionMod, _transform.up);
                }

                // Move player left and right
                var v = Velocity;
                v += Heading * (Blocking ? moveSpeed / 2 : moveSpeed) * Mathf.Abs(HDelta) * Time.deltaTime;
                Velocity = v;
            }        
        }

        /// <summary>
        /// Handles player jump input, velocity and animations.
        /// </summary>
        private void ProcessJumping()
        {
            if(Grounded)
            {
                doubleJumping = false;
            }

            if(Input.GetAxisRaw("Jump") != 0.0f && Grounded)
            {
                jumpPressed = true;
                Velocity = _transform.up * jumpStrength * Time.deltaTime;
                Animator.Play("Jump", 0, 0.0f);
            }
            else if(Input.GetAxisRaw("Jump") != 0.0f && !Grounded)
            {
                if(!doubleJumping && !jumpPressed)
                {
                    doubleJumping = true;
                    Velocity = _transform.up * jumpStrength * Time.deltaTime;
                    Animator.Play("Jump", 0, 0.0f);
                }
            }

            if(Input.GetAxisRaw("Jump") == 0.0f)
            {
                jumpPressed = false;
            }
        }

        /// <summary>
        /// Handles player dashing input and velocity vector.
        /// </summary>
        private void ProcessDashing()
        {
            if(!Dashing && dashTime > 0.0f)
            {
                dashTime -= Time.deltaTime;
                return;
            }

            if(Input.GetAxisRaw("Dash") != 0.0f && !Dashing && !dashPressed && HDelta != 0.0f)
            {
                dashPressed = true;
                Dashing = true;
                dashHeading = Heading * dashStrength;

                // TODO: Stamina drain and check later on.
            }

            if(Input.GetAxisRaw("Dash") == 0.0f)
            {
                dashPressed = false;
            }
        }


        /// <summary>
        /// Handles player blocking input and blocking collider activation/deactivation.
        /// </summary>
        private void ProcessBlocking()
        {
            Blocking = Input.GetAxisRaw("Block") == 1.0f && Grounded;

            // TODO: Blocking collider activation and deactivation.
        }

        /// <summary>
        /// Handles applying gravity to the player.
        /// </summary>
        private void ProcessGravity()
        {
            Velocity += Vector3.down * gravityStrength * Time.deltaTime;
        }

        /// <summary>
        /// Sets the velocity for the player.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void SetVelocity(object x = null, object y = null, object z = null)
        {
            var v = Velocity;
            if (x != null) v.x = (float) x;
            if (y != null) v.y = (float) y;
            if (z != null) v.z = (float) z;
            Velocity = v;
        }

        /// <summary>
        /// Gets the distance from the players feet to the ground (Max 100 units).
        /// If the player is more than 100 units from the ground then this just returns 100. (Not even close to ground :P)
        /// </summary>
        /// <returns></returns>
        private float GetDistanceToGround()
        {
            return CastRayToGround().distance;
        }

        private RaycastHit CastRayToGround()
        {
            RaycastHit hit;
            Physics.Linecast(_transform.position, _transform.position + Vector3.down * 100.0f, out hit);
            return hit;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var headingStart = transform.position + transform.up * 2.5f;
            var heading = headingStart + transform.forward * 2.0f;

            Gizmos.DrawLine(headingStart, heading);
        }
    }
}
