using System.Collections;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Camera
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraRotationTrigger : MonoBehaviour
    {
        public Vector3 targetRotation;

        [Tooltip("Time in seconds to rotate the camera to the targer rotation specified.")]
        public float rotationTime = 1.0f;

        [Tooltip("The curve used for smoothing the interpolation when rotation the camera.")]
        public AnimationCurve curve;

        public CameraTriggerBehaviour behaviour = CameraTriggerBehaviour.None;

        [Tooltip("Collection of rotation triggers to cancel if this trigger is activated.")]
        public CameraRotationTrigger[] cancelOut;

        private PlayerCameraController cameraController;

        private RotationInterpolator interpolator;

        private void Start()
        {
            cameraController = UnityEngine.Camera.main.gameObject.GetComponent<PlayerCameraController>();

            if(cameraController == null)
            {
                Debug.LogError("[CameraRotationTrigger] -> No camera controller detected in scene!");
            }
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (cameraController == null) return;

            if(_collider.gameObject.CompareTag("Player"))
            {
                StopAllCoroutines();

                foreach(var trigger in cancelOut)
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

                StartCoroutine(RotateCamera());
            }
        }

        private IEnumerator RotateCamera()
        {
            interpolator = new RotationInterpolator(cameraController.transform.rotation.eulerAngles, targetRotation, rotationTime, curve);

            while(!interpolator.HasFinished())
            {
                cameraController.transform.rotation = Quaternion.Euler(interpolator.Interpolate());
                yield return null;
            }
        }

        /// <summary>
        /// Unity Editor only function to force the required box collider component to be a trigger.
        /// </summary>
        private void Reset()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
