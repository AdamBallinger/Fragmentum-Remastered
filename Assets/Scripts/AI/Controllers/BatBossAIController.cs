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
            
        }
    }
}
