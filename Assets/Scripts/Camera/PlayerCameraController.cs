using Scripts.Player;
using UnityEngine;

namespace Scripts.Camera
{
    [ExecuteInEditMode]
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

            newCamPos = Vector3.MoveTowards(newCamPos, targetPos, speed * Time.deltaTime);
            newCamPos.y = anchorY;

            _transform.position = newCamPos;
        }
    }
}
