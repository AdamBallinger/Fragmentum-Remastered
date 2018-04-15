using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Utils
{
    public class TriggerEventCallback : MonoBehaviour
    {
        [Tooltip("If enabled, callbacks will trigger on collisions within the same gameobject.")]
        public bool enableSameObjectCallbacks = false;

        [SerializeField]
        private TriggerEvent triggerEnterEvent = null;

        [SerializeField]
        private TriggerEvent triggerStayEvent = null;

        [SerializeField]
        private TriggerEvent triggerExitEvent = null;

        private void OnTriggerEnter(Collider _collider)
        {
            if(ShouldInvoke(_collider))
            {
                triggerEnterEvent?.Invoke(_collider);
            }
        }

        private void OnTriggerStay(Collider _collider)
        {
            if(ShouldInvoke(_collider))
            {
                triggerStayEvent?.Invoke(_collider);
            }
        }

        private void OnTriggerExit(Collider _collider)
        {
            if(ShouldInvoke(_collider))
            {
                triggerExitEvent?.Invoke(_collider);
            }
        }

        private bool ShouldInvoke(Collider _collider)
        {
            return !enableSameObjectCallbacks && _collider.transform.root != transform.root
                   || enableSameObjectCallbacks && _collider.transform.root == transform.root;
        }
    }

    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    { }
}
