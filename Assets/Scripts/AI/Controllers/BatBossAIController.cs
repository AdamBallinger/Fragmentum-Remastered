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
        private Rotator rotator;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            rotator = GetComponent<Rotator>();

            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));
            actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(150.0f, 17.0f, 11.0f),
                moveSpeed, moveCurve));
            actionManager.EnqueAction(new AbilityAction(actionManager, flamethrowerAbility));
        }

        protected override void ControllerUpdate()
        {
            debugActionText.text = $"Action[1]: {actionManager.GetCurrentAction()?.GetType().Name}\n" +
                                   $"Action[2]: {actionManager.GetCurrentAction(2)?.GetType().Name}";

            debugActionText.gameObject.GetComponentInParent<Rotator>().rotateTarget = UnityEngine.Camera.main.transform.position;

            RotateToPosition(player.position);
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