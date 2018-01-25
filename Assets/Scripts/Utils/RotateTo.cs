using UnityEngine;

namespace Scripts.Utils
{
    public class RotateTo : MonoBehaviour
    {
        public Transform rotateTarget;

        private void Update()
        {
            if (rotateTarget == null)
                return;

            transform.LookAt(rotateTarget);
            var rot = transform.rotation.eulerAngles;
            rot.y -= 180.0f;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
