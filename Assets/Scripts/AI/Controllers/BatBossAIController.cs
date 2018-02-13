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
        public AbilityController basicAttackAbility;

        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            actionManager.SetDefaultAIAction(new IdleAction(actionManager, 0.0f));

            //actionManager.EnqueAction(new MoveAction(actionManager, transform.position, new Vector3(150.0f, 17.0f, 11.0f),
            //    moveSpeed, moveCurve, true));
            //actionManager.EnqueAction(new AbilityAction(actionManager, flamethrowerAbility));

            actionManager.EnqueAction(new IdleAction(actionManager, 1.0f));
            actionManager.EnqueAction(new AbilityAction(actionManager, basicAttackAbility));
        }

        protected override void ControllerUpdate()
        {
            debugActionText.text = $"Action[1]: {actionManager.GetCurrentAction()?.GetType().Name}\n" +
                                   $"Action[2]: {actionManager.GetCurrentAction(2)?.GetType().Name}";

            debugActionText.gameObject.GetComponentInParent<Rotator>().rotateTarget = UnityEngine.Camera.main.transform.position;

            RotateTowards(player.position);      
        }
    }
}