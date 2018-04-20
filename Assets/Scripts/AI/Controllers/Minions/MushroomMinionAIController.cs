using UnityEngine;

namespace Scripts.AI.Controllers.Minions
{
    public class MushroomMinionAIController : AIController
    {
        private Transform player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        protected override void ControllerUpdate()
        {
            RotateTowards(player.position);
        }

        public override void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}
