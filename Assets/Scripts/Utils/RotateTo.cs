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

        private void Update()
        {
            transform.LookAt(rotateTarget);
            var rot = transform.rotation.eulerAngles;

            if (!x)
            {
                rot.x = 0.0f;
            }

            if(!y)
            {
                rot.y = 0.0f;
            }

            if(!z)
            {
                rot.z = 0.0f;
            }

            if(applyYCorrection)
            {        
                rot.y -= 180.0f;      
            }

            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
