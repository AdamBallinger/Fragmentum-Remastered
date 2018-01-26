using Scripts.Abilities;
using Scripts.Abilities.Controllers;
using Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Scripts.AI.Controllers
{
    public class BatBossAIController : AIController
    {
        public TextMeshProUGUI debugActionText;

        public AnimationCurve moveCurve;
        public Vector3 testPos;
        public float speed;

        public AbilityData testAbilityData;
        public Vector3 testAbilStart;
        private Vector3 directionToPlayer;

        public Vector3 flamesStart, flamesEnd;
        public float flameSpeed;

        private Transform player;

        private RotateTo rotator;

        private void Start()
        {
            testAbilStart = transform.position + testAbilStart;
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            rotator = GetComponent<RotateTo>();

            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));

            //actionManager.EnqueAction(new MoveAction(actionManager, transform.position, testPos, speed, moveCurve));
            //actionManager.EnqueAction(new MoveAction(actionManager, testPos, transform.position, speed, moveCurve));
        }

        private bool b;
        protected override void ControllerUpdate()
        {
            debugActionText.text = $"Action[1]: {actionManager.GetCurrentAction()?.GetType().Name}\n" +
                                   $"Action[2]: {actionManager.GetCurrentAction(2)?.GetType().Name}";

            debugActionText.gameObject.GetComponentInParent<RotateTo>().rotateTarget = UnityEngine.Camera.main.transform.position;

            RotateToPosition(player.position);

            if (!b)
            {
                b = true;
                actionManager.EnqueAction(new AbilityAction(actionManager, 
                    new FlamethrowerAbilityController(testAbilityData, testAbilStart, flamesStart, flamesEnd, flameSpeed)), 2);
            }
        }

        private void RotateToPosition(Vector3 _target)
        {
            rotator.rotateTarget = _target;
        }

        public override void OnManagerActionFinished(AIAction _finishedAction)
        {
            if (_finishedAction is MoveAction)
            {
                actionManager.SetActionImmediate(new IdleAction(actionManager, 1.0f));
            }

            if (!actionManager.HasQueuedActions())
            {
                var randX = Random.Range(120, 170);
                var randY = Random.Range(14, 17);
                actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(randX, randY, 1.5f), speed, moveCurve));
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(GetComponent<Transform>().position + testAbilStart, Vector3.one);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(flamesStart, Vector3.one);
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(flamesEnd, Vector3.one);
        }
    }
}
