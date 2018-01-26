using Scripts.AI;
using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    public class FlamethrowerAbilityController : AbilityController
    {
        private Vector3 flameStartPos, flameEndPos;

        private float speed;

        private float distance;
        private float t;

        private GameObject abilityGO;

        public FlamethrowerAbilityController(AbilityData _abilityData, Vector3 _abilityOrigin,
            Vector3 _start, Vector3 _end, float _speed) 
            : base(_abilityData, _abilityOrigin)
        {
            flameStartPos = _start;
            flameEndPos = _end;
            speed = _speed;
            distance = Vector3.Distance(flameStartPos, flameEndPos);
            t = 0.0f;

            abilityGO = Object.Instantiate(AbilityData.particleSystem, AbilityOrigin, Quaternion.identity).gameObject;
        }

        public override bool Update(AbilityAction _abilityAction)
        {
            abilityGO.transform.SetParent(_abilityAction.ActionManager.Controller.transform.Find("joint1"));

            if(t >= 1.0f)
            {
                return true;
            }

            t += Time.deltaTime / (distance / speed);

            return false;
        }
    }
}
