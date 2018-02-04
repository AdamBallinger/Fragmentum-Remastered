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
        public float moveSpeed;

        public AbilityController flamethrowerAbility;

        private Transform player;
        private RotateTo rotator;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            rotator = GetComponent<RotateTo>();

            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));
            actionManager.EnqueAction(new AbilityAction(actionManager, flamethrowerAbility));

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
            }
        }

        private void RotateToPosition(Vector3 _target)
        {
            rotator.rotateTarget = _target;
        }

        public override void OnManagerActionFinished(AIAction _finishedAction)
        {
            //if (_finishedAction is MoveAction)
            //{
            //    actionManager.SetActionImmediate(new IdleAction(actionManager, 1.0f));
            //}

            //if (!actionManager.HasQueuedActions())
            //{
            //    var randX = Random.Range(120, 170);
            //    var randY = Random.Range(14, 17);
            //    actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(randX, randY, 1.5f), moveSpeed, moveCurve));
            //}
        }
    }
}