using UnityEngine;

namespace Scripts.AI.Controllers
{
    public class BatBossAIController : BaseAIController
    {

        public AnimationCurve moveCurve;
        public Vector3 testPos;
        public float speed;

        private void Start()
        {
            brain.EnqueAction(new MoveAction(brain, transform.position, testPos, speed, moveCurve));
            brain.EnqueAction(new MoveAction(brain, testPos, transform.position, speed, moveCurve));
        }

        protected override void ControllerUpdate()
        {
            
        }

        public override void OnBrainActionFinished(AIAction _finishedAction)
        {
            if(!brain.HasQueuedActions())
            {
                var randX = Random.Range(130, 165);
                var randY = Random.Range(9, 16);
                brain.EnqueAction(new MoveAction(brain, transform.position, new Vector3(randX, randY, 1.5f), speed, moveCurve));
            }
        }
    }
}
