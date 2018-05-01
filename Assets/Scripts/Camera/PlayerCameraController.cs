using Scripts.Player;
using UnityEngine;

namespace Scripts.Camera
{
    public class PlayerCameraController : MonoBehaviour
    {
        public bool enableFollow = true;

        public Transform playerTransform;

        public Vector3 cameraOffset;
        public float speed = 1.0f;

        private PlayerController playerController;

        private Transform _transform;

        private float anchorY;

        private void Start()
        {
            _transform = transform;
            anchorY = _transform.position.y;
        }

        private void LateUpdate()
        {
            if (playerTransform == null || !enableFollow) return;

            if(playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
                return;
            }

            var offset = cameraOffset;
            offset.x = playerController.Heading.x <= 0.0f ? -cameraOffset.x : cameraOffset.x;

            var newCamPos = _transform.position;
            var targetPos = playerTransform.position + offset;

            newCamPos.x = Mathf.MoveTowards(newCamPos.x, targetPos.x, speed * Time.deltaTime);
            newCamPos.z = Mathf.MoveTowards(newCamPos.z, targetPos.z, speed * Time.deltaTime);
            newCamPos.y = anchorY + offset.y;

            _transform.position = newCamPos;
        }
    }
}
