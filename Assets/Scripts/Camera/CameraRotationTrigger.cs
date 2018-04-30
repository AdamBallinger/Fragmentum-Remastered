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

        [Tooltip("Collection of rotation triggers to cancel if this trigger is activated.")]
        public CameraRotationTrigger[] cancelOut;

        private GameObject cameraObject;

        private RotationInterpolator interpolator;

        private void Start()
        {
            cameraObject = UnityEngine.Camera.main.gameObject;

            if(cameraObject == null)
            {
                Debug.LogError("[CameraRotationTrigger] -> No camera detected in scene!");
            }
        }

        private void OnTriggerEnter(Collider _collider)
        {
            if (cameraObject == null) return;

            if(_collider.gameObject.CompareTag("Player"))
            {
                StopAllCoroutines();

                foreach(var trigger in cancelOut)
                {
                    trigger.StopAllCoroutines();
                }

                StartCoroutine(RotateCamera());
            }
        }

        private IEnumerator RotateCamera()
        {
            interpolator = new RotationInterpolator(cameraObject.transform.rotation.eulerAngles, targetRotation, rotationTime, curve);

            while(!interpolator.HasFinished())
            {
                cameraObject.transform.rotation = Quaternion.Euler(interpolator.Interpolate());
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
