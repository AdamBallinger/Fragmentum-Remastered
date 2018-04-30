using UnityEngine;

namespace Scripts.Utils
{
    public class RotationInterpolator
    {
        private Vector3 startRotation;
        private Vector3 endRotation;

        private AnimationCurve curve;

        private float t;

        private float rate;

        /// <summary>
        /// Linearly interpolate between a given start and end rotation.
        /// </summary>
        /// <param name="_start">Where to start interpolating from.</param>
        /// <param name="_end">Where to interpolate to.</param>
        /// <param name="_speed">Time in seconds it takes to interpolate from start to end.</param>
        /// <param name="_curve">Smoothing curve to apply to the interpolation.</param>
        public RotationInterpolator(Vector3 _start, Vector3 _end, float _speed, AnimationCurve _curve)
        {
            startRotation = _start;
            endRotation = _end;
            curve = _curve;
            t = 0.0f;
            rate = _speed;
        }

        /// <summary>
        /// Interpolates and returns the euler rotation between the given start and end.
        /// </summary>
        /// <returns></returns>
        public Vector3 Interpolate()
        {
            var x = Mathf.LerpAngle(startRotation.x, endRotation.x, curve.Evaluate(t));
            var y = Mathf.LerpAngle(startRotation.y, endRotation.y, curve.Evaluate(t));
            var z = Mathf.LerpAngle(startRotation.z, endRotation.z, curve.Evaluate(t));

            t += Time.deltaTime / rate;
            return new Vector3(x, y, z);
        } 

        /// <summary>
        /// Sets the starting position for the interpolator.
        /// </summary>
        /// <param name="_start"></param>
        public void SetStart(Vector3 _start)
        {
            startRotation = _start;
        }

        public void Reset()
        {
            t = 0.0f;
        }

        public bool HasFinished()
        {
            return t >= 1.0f;
        }
    }
}
