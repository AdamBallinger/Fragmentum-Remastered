using Scripts.Utils;
using UnityEngine;

namespace Scripts.Camera
{
    public class CameraShake : MonoBehaviour
    {
        private bool isShaking;

        /// <summary>
        /// Distance the shake moves the camera from its original position.
        /// </summary>
        private float shakeMagnitude;

        /// <summary>
        /// The speed that the shake moves the camera.
        /// </summary>
        private float shakeIntensity;

        /// <summary>
        /// The time in seconds the shake lasts for.
        /// </summary>
        private float shakeDuration;

        /// <summary>
        /// The current time in seconds the camera has been shaking for.
        /// </summary>
        private float shakeTime;

        private Vector3 originalPosition;
        private Vector3 newPosition;

        private Transform cameraTransform;

        private void Start()
        {
            isShaking = false;
            shakeDuration = 0.0f;

            cameraTransform = transform;
            StartShake(5.0f);
        }

        public void StartShake(float _duration, float _magnitude = 1.0f, float _intensity = 10.0f)
        {
            isShaking = true;
            shakeDuration = _duration;
            shakeMagnitude = _magnitude;
            shakeIntensity = _intensity;
            shakeTime = 0.0f;
            originalPosition = cameraTransform.localPosition;
            newPosition = originalPosition;
        }

        private void LateUpdate()
        {
            if (isShaking)
            {
                Shake();
            }
        }

        private void Shake()
        {
            if (shakeTime >= shakeDuration)
            {
                isShaking = false;
                cameraTransform.localPosition = originalPosition;
                return;
            }

            if (Vector3.Distance(newPosition, cameraTransform.localPosition) <= shakeMagnitude)
            {
                newPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            }

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPosition, shakeIntensity * Time.deltaTime);

            shakeTime += Time.deltaTime;
        }
    }
}