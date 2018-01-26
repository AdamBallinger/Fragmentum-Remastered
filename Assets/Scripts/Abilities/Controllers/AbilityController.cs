using System;
using Scripts.AI;
using Scripts.AI.Controllers;
using UnityEngine;

namespace Scripts.Abilities.Controllers
{
    [Serializable]
    public abstract class AbilityController
    {
        public AbilityData AbilityData { get; }

        protected Vector3 AbilityOrigin { get; }

        protected AbilityController(AbilityData _abilityData, Vector3 _abilityOrigin)
        {
            AbilityData = _abilityData;
            AbilityOrigin = _abilityOrigin;
        }

        public abstract bool Update(AbilityAction _abilityAction);
    }
}
