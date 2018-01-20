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
            brain.SetActionImmediate(new MoveAction(brain, testPos, speed, moveCurve));
        }

        protected override void ControllerUpdate()
        {
            
        }

        public override void OnBrainActionFinished(AIAction _finishedAction)
        {

        }
    }
}
