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

        public CameraTriggerBehaviour behaviour = CameraTriggerBehaviour.Pickup;

        public CameraOffsetTrigger[] cancelOut;

        private PlayerCameraController cameraController;

        private FloatInterpolator offsetInterpolator;
        private Vector3Interpolator moveInterpolator;

        private void Start()
        {
            cameraController = GameObject.FindGameObjectWithTag("Camera_Container")
                                         ?.GetComponent<PlayerCameraController>();

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
                        break;

                    case CameraTriggerBehaviour.Pickup:
                        cameraController.enableFollow = true;
                        break;
                }

                StartCoroutine(MoveCamera());
                StartCoroutine(OffsetCamera());
            }
        }

        private IEnumerator OffsetCamera()
        {
            offsetInterpolator = new FloatInterpolator(cameraController.xOffset, cameraOffset.x, moveTime, curve);

            while (!offsetInterpolator.HasFinished())
            {
                cameraController.xOffset = offsetInterpolator.Interpolate();

                yield return null;
            }
        }

        private IEnumerator MoveCamera()
        {
            moveInterpolator = new Vector3Interpolator(cameraController.transform.position,
                                                       transform.position + cameraOffset, moveTime, curve, true);

            while (!moveInterpolator.HasFinished())
            {
                var pos = cameraController.transform.position;
                var interpolated = moveInterpolator.Interpolate();

                if (behaviour == CameraTriggerBehaviour.Drop)
                {
                    pos.x = interpolated.x;
                }

                pos.y = interpolated.y;
                pos.z = interpolated.z;

                cameraController.transform.position = pos;

                yield return null;
            }
        }

        private void Reset()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + cameraOffset, Vector3.one / 2.0f);
            Gizmos.DrawCube(transform.position + new Vector3(-cameraOffset.x, cameraOffset.y, cameraOffset.z),
                            Vector3.one / 2.0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0.0f, cameraOffset.y, cameraOffset.z));
            Gizmos.DrawLine(transform.position + new Vector3(0.0f, cameraOffset.y, cameraOffset.z),
                            transform.position + cameraOffset);
            Gizmos.DrawLine(transform.position + new Vector3(0.0f, cameraOffset.y, cameraOffset.z),
                            transform.position + new Vector3(-cameraOffset.x, cameraOffset.y, cameraOffset.z));

            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + new Vector3(0.0f, cameraOffset.y, cameraOffset.z), Vector3.one);
        }
    }
}