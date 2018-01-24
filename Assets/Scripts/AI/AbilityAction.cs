namespace Scripts.AI
{
    public class AbilityAction : AIAction
    {

        private Ability ability;

        public AbilityAction(AIActionManager _actionManager, Ability _ability) : base(_actionManager)
        {
            ability = _ability;
        }

        public override void Update()
        {
            
        }
    }
}
