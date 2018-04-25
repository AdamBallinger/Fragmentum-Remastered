using UnityEngine;

namespace Scripts.AI
{
    public class ChaseAction : AIAction
    {
        private Transform target, transform;

        private float speed;

        public ChaseAction(AIActionManager _actionManager, Transform _target, float _speed) : base(_actionManager)
        {
            target = _target;
            transform = ActionManager.Controller.transform;
            speed = _speed;
        }

        public override void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, transform.position.y, target.position.z), speed * Time.deltaTime);
        }
    }
}
