using UnityEngine;

namespace Scripts.Camera
{
    [ExecuteInEditMode]
    public class PlayerCameraController : MonoBehaviour
    {

        public Transform playerTransform;

        public Vector3 cameraOffset;
        public float offsetScale = 1.0f;
        public float speed = 1.0f;       

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (playerTransform == null) return;

            var offset = cameraOffset;
            offset.x = playerTransform.forward.x <= 0.0f ? -cameraOffset.x : cameraOffset.x;

            //var newCamPos = Vector3.MoveTowards(_transform.position,
            //    playerTransform.position + offset * offsetScale, speed * Time.deltaTime);

            var newCamPos = _transform.position;

            newCamPos.x = Mathf.MoveTowards(newCamPos.x, 
                playerTransform.position.x + offset.x * offsetScale, speed * Time.deltaTime);
            newCamPos.z = Mathf.MoveTowards(newCamPos.z,
                playerTransform.position.z + offset.z * offsetScale, speed * Time.deltaTime);

            newCamPos.y = playerTransform.position.y + offset.y;

            _transform.position = newCamPos;
        }
    }
}
