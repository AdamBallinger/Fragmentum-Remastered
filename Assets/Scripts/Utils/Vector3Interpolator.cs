using UnityEngine;

namespace Scripts.Utils
{
    public class Vector3Interpolator
    {
        private Vector3 initialPosition;
        private Vector3 targetPosition;

        private AnimationCurve curve;

        private float speed;

        private float t;

        private float rate;

        public Vector3Interpolator(Vector3 _start, Vector3 _end, float _speed, AnimationCurve _curve)
        {
            initialPosition = _start;
            targetPosition = _end;
            speed = _speed;
            CalculateTRate();
            curve = _curve;
            t = 0.0f;
        }

        /// <summary>
        /// Interpolates and returns the vector between the given start and end.
        /// </summary>
        /// <returns></returns>
        public Vector3 Interpolate()
        {
            var interpolated = Vector3.Lerp(initialPosition, targetPosition, curve.Evaluate(t));
            t += Time.deltaTime / rate;
            return interpolated;
        }

        private void CalculateTRate()
        {
            rate = Vector3.Distance(initialPosition, targetPosition) / speed;
        }

        /// <summary>
        /// Sets the starting position for the interpolator.
        /// </summary>
        /// <param name="_start"></param>
        public void SetStart(Vector3 _start)
        {
            initialPosition = _start;
            CalculateTRate();
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
