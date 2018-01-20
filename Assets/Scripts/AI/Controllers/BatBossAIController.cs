using UnityEngine;

namespace Scripts.AI.Controllers
{
    public class BatBossAIController : GenericAIController
    {

        public AnimationCurve moveCurve;
        public Vector3 testPos;
        public float time;

        private void Start()
        {
            brain.SetActionImmediate(new MoveAction(brain, testPos, time, moveCurve));
        }

        protected override void ControllerUpdate()
        {
            
        }

        public override void OnBrainActionFinished(AIAction _finishedAction)
        {

        }
    }
}
