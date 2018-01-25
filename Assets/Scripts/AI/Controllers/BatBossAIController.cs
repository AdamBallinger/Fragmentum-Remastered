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

        public Ability testAbility;
        public Vector3 testAbilStart;
        private Vector3 directionToPlayer;

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));

            actionManager.EnqueAction(new MoveAction(actionManager, transform.position, testPos, speed, moveCurve));
            //actionManager.EnqueAction(new MoveAction(actionManager, testPos, transform.position, speed, moveCurve));
        }

        private bool b;
        protected override void ControllerUpdate()
        {
            debugActionText.text = $"Action[1]: {actionManager.GetCurrentAction()?.GetType().Name}\n" +
                                   $"Action[2]: {actionManager.GetCurrentAction(2)?.GetType().Name}";

            var playerPos = player.position;

            RotateToPosition(playerPos);

            directionToPlayer = (playerPos - (transform.position + testAbilStart)).normalized;

            if(!b)
            {
                b = true;
                actionManager.EnqueAction(new AbilityAction(actionManager, testAbility, transform.position + testAbilStart, directionToPlayer), 2);
            }
        }

        private void RotateToPosition(Vector3 _target)
        {
            transform.LookAt(_target, Vector3.up);

            var euler = transform.rotation.eulerAngles;
            euler.x = 0.0f;
            euler.z = 0.0f;

            transform.rotation = Quaternion.Euler(euler);
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
            var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            directionToPlayer = (playerPos - (GetComponent<Transform>().position + testAbilStart)).normalized;

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(GetComponent<Transform>().position + testAbilStart, Vector3.one);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(GetComponent<Transform>().position + testAbilStart,
                GetComponent<Transform>().position + testAbilStart + directionToPlayer);
        }
    }
}
