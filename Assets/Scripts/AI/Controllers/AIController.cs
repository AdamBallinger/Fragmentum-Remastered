using Scripts.Combat;
using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.AI.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rotator))]
    [RequireComponent(typeof(HealthSystem))]
    public abstract class AIController : MonoBehaviour, IDamageable
    {
        public bool usesGravity;

        public bool clampOnAxis;
        public PlayerMoveAxis clampedAxis = PlayerMoveAxis.Z;
        private float clampedValue;

        [HideInInspector]
        public new Transform transform;

        public CharacterController Controller { get; private set; }

        protected Animator Animator { get; private set; }

        public Rotator Rotator { get; private set; }

        protected AIActionManager ActionManager { get; private set; }

        protected HealthSystem healthSystem;

        /// <summary>
        /// Property defines if the AIController controls the rotation of the AI. Useful for allowing external components
        /// to control the rotation of the AI.
        /// </summary>
        public bool ControlsRotation { get; set; } = true;

        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
            ActionManager = new AIActionManager(this);
            transform = GetComponent<Transform>();
            Animator = GetComponent<Animator>();
            Rotator = GetComponent<Rotator>();
            healthSystem = GetComponent<HealthSystem>();

            if(clampOnAxis)
            {
                switch(clampedAxis)
                {
                    case PlayerMoveAxis.X:
                        clampedValue = transform.position.x;
                        break;
                    case PlayerMoveAxis.Z:
                        clampedValue = transform.position.z;
                        break;
                }
            }
        }
        
        private void Update()
        {
            ControllerUpdate();
            ActionManager?.Update();

            if(clampOnAxis)
            {
                var currentPos = transform.position;

                switch (clampedAxis)
                {
                    case PlayerMoveAxis.X:
                        currentPos.x = clampedValue;
                        break;

                    case PlayerMoveAxis.Z:
                        currentPos.z = clampedValue;
                        break;
                }

                transform.position = currentPos;
            }
        }

        /// <summary>
        /// Rotates the AI towards the given target position.
        /// </summary>
        /// <param name="_target"></param>
        protected void RotateTowards(Vector3 _target)
        {
            if(!ControlsRotation)
            {
                return;
            }

            Rotator.rotateTarget = _target;
        }

        /// <summary>
        /// Handles custom controller update logic.
        /// </summary>
        protected abstract void ControllerUpdate();

        /// <summary>
        /// Event for when the action manager starts a new action.
        /// </summary>
        /// <param name="_action"></param>
        public virtual void OnManagerActionStart(AIAction _action) { }

        /// <summary>
        /// Event for when the action manager finished its last action.
        /// </summary>
        public virtual void OnManagerActionFinished(AIAction _action) { }

        public HealthSystem GetHealth()
        {
            return healthSystem;
        }

        public virtual void OnDamageReceived(int _damage) { }

        public abstract void OnDeath();
    }
}
