using System.Collections;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Camera
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraOffsetTrigger : MonoBehaviour
    {
        public Vector3 cameraOffset;

        [Tooltip("Time in seconds to complete the move.")]
        public float moveTime = 1.0f;

        public AnimationCurve curve;

        public CameraTriggerBehaviour behaviour = CameraTriggerBehaviour.None;

        public CameraOffsetTrigger[] cancelOut;

        private PlayerCameraController cameraController;

        private Vector3Interpolator interpolator;

        private void Start()
        {
            cameraController = UnityEngine.Camera.main.GetComponent<PlayerCameraController>();

            if (cameraController == null)
            {
                Debug.LogError("[CameraOffsetTrigger] -> Failed to find camera controller in scene!");
            }
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (cameraController == null) return;

            if (_collider.gameObject.CompareTag("Player"))
            {
                StopAllCoroutines();

                foreach (var trigger in cancelOut)
                {
                    trigger.StopAllCoroutines();
                }

                switch (behaviour)
                {
                    case CameraTriggerBehaviour.Drop:
                        cameraController.enableFollow = false;
                        StartCoroutine(MoveCamera());
                        break;

                    case CameraTriggerBehaviour.Pickup:
                        cameraController.enableFollow = true;
                        StartCoroutine(OffsetCamera());
                        break;
                }
            }
        }

        private IEnumerator OffsetCamera()
        {
            interpolator = new Vector3Interpolator(cameraController.cameraOffset, cameraOffset, moveTime, curve, true);

            while (!interpolator.HasFinished())
            {
                cameraController.cameraOffset = interpolator.Interpolate();

                yield return null;
            }
        }

        private IEnumerator MoveCamera()
        {
            interpolator = new Vector3Interpolator(cameraController.transform.position, transform.position + cameraOffset, moveTime, curve, true);

            while(!interpolator.HasFinished())
            {
                cameraController.transform.position = interpolator.Interpolate();

                yield return null;
            }
        }

        private void Reset()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
