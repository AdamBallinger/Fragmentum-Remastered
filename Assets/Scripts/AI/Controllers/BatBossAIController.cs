using TMPro;
using UnityEngine;

namespace Scripts.AI.Controllers
{
    public class BatBossAIController : AIController
    {
        public TextMeshProUGUI actionText;

        public AnimationCurve moveCurve;
        public Vector3 testPos;
        public float speed;

        private void Start()
        {
            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));

            actionManager.EnqueAction(new MoveAction(actionManager, transform.position, testPos, speed, moveCurve));
            actionManager.EnqueAction(new MoveAction(actionManager, testPos, transform.position, speed, moveCurve));
        }

        protected override void ControllerUpdate()
        {
            actionText.text = $"Action: {actionManager.GetCurrentAction().GetType().Name}";
        }

        public override void OnManagerActionFinished(AIAction _finishedAction)
        {
            if(!actionManager.HasQueuedActions())
            {
                var randX = Random.Range(130, 165);
                var randY = Random.Range(9, 16);
                actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(randX, randY, 1.5f), speed, moveCurve));
            }
        }
    }
}
