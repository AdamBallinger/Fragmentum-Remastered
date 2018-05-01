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

        private void Start()
        {
            _transform = transform;
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
            offset.x = playerTransform.forward.x <= 0.0f ? -cameraOffset.x : cameraOffset.x;

            var newCamPos = _transform.position;
            var oldY = newCamPos.y;

            newCamPos = Vector3.MoveTowards(newCamPos, playerTransform.position + offset, speed * Time.deltaTime);
            newCamPos.y = oldY;

            _transform.position = newCamPos;
        }
    }
}
