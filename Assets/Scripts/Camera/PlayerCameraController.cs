using Scripts.Player;
using UnityEngine;

namespace Scripts.Camera
{
    public class PlayerCameraController : MonoBehaviour
    {
        public bool enableFollow = true;

        public Transform playerTransform;

        public float xOffset;
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

            var offset = playerController.Heading.x <= 0.0f ? -xOffset : xOffset;

            var newCamPos = _transform.position;
            var targetPos = playerTransform.position.x + offset;

            newCamPos.x = Mathf.MoveTowards(newCamPos.x, targetPos, speed * Time.deltaTime);

            _transform.position = newCamPos;
        }
    }
}
