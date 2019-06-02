using System;
using Scripts.Combat;
using TMPro;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour, IDamageable, IDamageProvider
    {
        private CharacterController Controller { get; set; }

        private PlayerInputController InputController { get; set; }

        private Animator Animator { get; set; }

        public Vector3 Heading { get; set; }

        public Vector3 Velocity { get; private set; }

        public bool Dashing { get; private set; }

        public bool Blocking { get; private set; }

        public bool Grounded => Controller.isGrounded;

        public bool Falling => !Grounded && Velocity.y < 0.0f && GetDistanceToGround() >= fallDistanceThreshold;

        public bool ControlsEnabled { get; set; } = true;

        [Header("Movement Settings")]
        public float moveSpeed = 1.0f;

        [Range(0.0f, 1.0f)]
        [Tooltip("Affects the movement speed of the player whilst blocking.")]
        public float blockingModifier = 0.5f;

        public float jumpStrength = 1.0f;
        public int allowedJumps = 2;
        public float dashStrength = 1.0f;

        [Range(0, 2)]
        public float dashDuration = 1.0f;

        [Range(0, 2)]
        [Tooltip("Delay in seconds between being able to dash repeatedly.")]
        public float dashDelay = 0.5f;

        [Header("Clamp Settings")]
        [Tooltip("The axis in which the player moves along. The position of the player will be clamped on the opposite axis." +
                 "E.g. Movement axis is X and the Z axis will be clamped so the player only moves along the X.")]
        public PlayerMoveAxis playerMovementAxis = PlayerMoveAxis.X;

        [Header("Physics Settings")]
        [Range(0.0f, 10.0f)]
        public float gravityStrength = 1.0f;

        [Tooltip("The distance that the player must be from the ground before the controller thinks the player is falling.")]
        public float fallDistanceThreshold = 1.0f;

        [Header("Combat Settings")]
        public int dashDamage = 1;

        public GameObject shield;
        public GameObject dashCollider;

        [Header("Visual Settings")]
        public float meshRotationSpeed = 8.0f;

        private Vector3 dashVelocity;

        private bool canJump;
        private int jumpCount;

        // The current dash time.
        private float dashTime;

        private float clampedAxisValue;

        private Quaternion facingRotation;

        private HealthSystem healthSystem;

        private Transform _transform;

        private void Start()
        {
            Controller = GetComponentInParent<CharacterController>();
            InputController = GetComponent<PlayerInputController>();
            Animator = GetComponent<Animator>();
            healthSystem = GetComponent<HealthSystem>();

            _transform = transform.root;
            facingRotation = _transform.rotation;
            Heading = _transform.forward;

            switch (playerMovementAxis)
            {
                case PlayerMoveAxis.X:
                    clampedAxisValue = _transform.position.z;
                    break;

                case PlayerMoveAxis.Z:
                    clampedAxisValue = _transform.position.x;
                    break;
            }
        }

        private void SetAnimations()
        {
            Animator?.SetFloat("animSpeedMod", InputController.HDelta);
            Animator?.SetBool("isGrounded", Grounded);
            Animator?.SetBool("isRunning", Math.Abs(InputController.HDelta) > 0.0f);
            Animator?.SetBool("isDashing", Dashing);
            Animator?.SetBool("isFalling", Falling);
            Animator?.SetBool("isBlocking", Blocking);
        }

        private void Update()
        {
            if (Math.Abs(InputController.HDelta) > 0.0f)
            {
                Heading = InputController.HDelta < 0.0f ? Vector3.left : Vector3.right;
            }

            if (ControlsEnabled)
            {
                ProcessJumping();
                ProcessDashing();
                ProcessBlocking();
                ProcessMovement();
            }

            ProcessGravity();

            SetAnimations();

            var flags = Controller.Move(Velocity);

            // Reset Y velocity if the player hits something above.
            if (flags == CollisionFlags.CollidedAbove)
            {
                SetVelocity(y: -0.05f);
            }

            CorrectPosition();

            // Reset Y velocity if grounded so debug text shows correct values.
            SetVelocity(y: Grounded ? 0.0f : Velocity.y);

            // Reset X/Z velocity so the player only moves during input.
            SetVelocity(0.0f, z: 0.0f);
        }

        /// <summary>
        /// Handles all movement for the player and calculates the final velocity for the player (pre-gravity) this frame
        /// to be applied to the character controller.
        /// </summary>
        private void ProcessMovement()
        {
            if (Dashing)
            {
                Velocity = dashVelocity * Time.deltaTime;
                dashTime += Time.deltaTime;

                if (dashTime >= dashDuration)
                {
                    dashTime = dashDelay;
                    Dashing = false;
                }
            }
            else
            {
                // Rotate player to face where they will move.
                if (Math.Abs(InputController.HDelta) > 0.0f)
                {
                    var directionMod = InputController.HDelta < 0.0f ? -1.0f : 1.0f;
                    facingRotation = Quaternion.AngleAxis(90.0f * directionMod, _transform.up);
                }

                _transform.rotation = Quaternion.Slerp(_transform.rotation, facingRotation, meshRotationSpeed * Time.deltaTime);

                // Move player left and right
                var v = Velocity;
                v += Heading * (Blocking ? moveSpeed * blockingModifier : moveSpeed) * Mathf.Abs(InputController.HDelta) *
                     Time.deltaTime;
                Velocity = v;
            }
        }

        /// <summary>
        /// Handles player jump input, velocity and animations.
        /// </summary>
        private void ProcessJumping()
        {
            // Reset jumping variables when the player is grounded.
            if (Grounded)
            {
                jumpCount = 0;
                canJump = true;
            }

            if (InputController.Jump && canJump)
            {
                if (jumpCount < allowedJumps)
                {
                    Velocity = _transform.up * (jumpCount == 0 ? jumpStrength : jumpStrength * 0.8f) * Time.deltaTime;
                    Animator.Play("Jump", 0, 0.0f);

                    jumpCount++;
                }
                else
                {
                    canJump = false;
                }
            }
        }

        /// <summary>
        /// Handles player dashing input and velocity vector.
        /// </summary>
        private void ProcessDashing()
        {
            if (!Dashing && dashTime > 0.0f)
            {
                dashTime -= Time.deltaTime;
                return;
            }

            if (InputController.Dash && !Dashing && Math.Abs(InputController.HDelta) > 0.0f)
            {
                Dashing = true;
                dashVelocity = Heading * dashStrength;

                // TODO: Stamina drain and check later on.
            }

            dashCollider.SetActive(Dashing);
        }

        /// <summary>
        /// Handles player blocking input and blocking collider activation/deactivation.
        /// </summary>
        private void ProcessBlocking()
        {
            Blocking = InputController.Block && Grounded;
            shield.SetActive(Blocking);
        }

        /// <summary>
        /// Handles applying gravity to the player.
        /// </summary>
        private void ProcessGravity()
        {
            Velocity += Vector3.down * (gravityStrength * Time.deltaTime);
        }

        /// <summary>
        /// Corrects the player position based on the player movement axis.
        /// </summary>
        private void CorrectPosition()
        {
            var pos = _transform.position;

            switch (playerMovementAxis)
            {
                case PlayerMoveAxis.X:
                    pos.z = clampedAxisValue;
                    break;
                case PlayerMoveAxis.Z:
                    pos.x = clampedAxisValue;
                    break;
            }

            _transform.position = pos;
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

        /// <summary>
        /// Event called when the player dashes into a target.
        /// </summary>
        /// <param name="_collider"></param>
        public void OnDashHit(Collider _collider)
        {
            CombatSystem.ProcessDamage(gameObject, _collider.gameObject, AttackType.Player_Dash);
        }

        /// <summary>
        /// Event called when the player jumps on the head of a target.
        /// </summary>
        /// <param name="_collider"></param>
        public void OnFeetHit(Collider _collider)
        {
            if (_collider.gameObject.CompareTag("AI_Head"))
            {
                Debug.Log("Hit head");
                CombatSystem.ProcessDamage(gameObject, _collider.gameObject, AttackType.Head_Hit);
                Velocity = _transform.up * (jumpStrength * 0.5f) * Time.deltaTime;
                Animator.Play("Jump", 0, 0.0f);
            }
        }

        public HealthSystem GetHealth()
        {
            return healthSystem;
        }

        public void OnDamageReceived(int _damage)
        {
            if (Math.Abs(InputController.HDelta) > 0.0f || !Grounded)
            {
                Animator?.Play("Running Damage");
            }
            else
            {
                Animator?.Play("Standing Damage");
            }
        }

        public void OnDeath()
        {
            Animator?.SetBool("isDead", true);
            //TODO: Actual death event...
        }

        public int GetDamage()
        {
            return dashDamage;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var headingStart = transform.position + transform.up * 2.5f;
            var heading = headingStart + transform.forward * 2.0f;

            Gizmos.DrawLine(headingStart, heading);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.4f);
        }
    }

    public enum PlayerMoveAxis
    {
        X,
        Z
    }
}