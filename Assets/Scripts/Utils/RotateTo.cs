using UnityEngine;

namespace Scripts.Utils
{
    public class RotateTo : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 rotateTarget;

        public bool applyYCorrection = true;

        public bool x = true;
        public bool y = true;
        public bool z = true;

        public bool smoothRotation = true;
        public float rotateSpeed = 1.0f;

        private void Update()
        {
            var originalRotation = transform.rotation;
            transform.LookAt(rotateTarget);
            var targetRotation = transform.rotation.eulerAngles;

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
                transform.rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(targetRotation), rotateSpeed * Time.deltaTime);
                return;
            }

            transform.rotation = Quaternion.Euler(targetRotation);
        }
    }
}
