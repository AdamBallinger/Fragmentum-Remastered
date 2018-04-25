using UnityEngine;

namespace Scripts.Utils
{
    public class Rotator : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 rotateTarget;

        [Tooltip("If specified, this object will be rotated using this rotator instead of the object the component is placed on. " +
                 "Useful for controlling specific children of a gameobject.")]
        public GameObject rotating;

        public bool applyYCorrection = true;

        public bool x = true;
        public bool y = true;
        public bool z = true;

        public bool smoothRotation = true;
        public float rotateSpeed = 1.0f;

        private void Update()
        {
            var originalRotation = GetRotated().rotation;
            GetRotated().LookAt(rotateTarget);
            var targetRotation = GetRotated().rotation.eulerAngles;

            if (!x)
            {
                targetRotation.x = 0.0f;
            }

            if (!y)
            {
                targetRotation.y = 0.0f;
            }

            if(!z)
            {
                targetRotation.z = 0.0f;
            }

            if(applyYCorrection)
            {        
                targetRotation.y -= 180.0f;
            }

            if(smoothRotation)
            {
                GetRotated().rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(targetRotation), rotateSpeed * Time.deltaTime);
                return;
            }

            GetRotated().rotation = Quaternion.Euler(targetRotation);
        }

        private Transform GetRotated()
        {
            return rotating != null ? rotating.transform : transform;
        }
    }
}
