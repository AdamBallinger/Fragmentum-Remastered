using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Utils
{
    public class TriggerEventCallback : MonoBehaviour
    {
        [SerializeField]
        private TriggerEvent triggerEnterEvent = null;

        [SerializeField]
        private TriggerEvent triggerStayEvent = null;

        [SerializeField]
        private TriggerEvent triggerExitEvent = null;

        private void OnTriggerEnter(Collider _collider)
        {
            triggerEnterEvent?.Invoke(_collider);
        }

        private void OnTriggerStay(Collider _collider)
        {
            triggerStayEvent?.Invoke(_collider);
        }

        private void OnTriggerExit(Collider _collider)
        {
            triggerExitEvent?.Invoke(_collider);
        }
    }

    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    { }
}
