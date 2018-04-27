using UnityEngine;

namespace Scripts.Utils
{
    public class FloatInterpolator
    {
        private float initial;
        private float target;

        private AnimationCurve curve;

        private float speed;

        private float t;

        private float rate;

        public FloatInterpolator(float _start, float _end, float _speed, AnimationCurve _curve)
        {
            initial = _start;
            target = _end;
            speed = _speed;
            CalculateTRate();
            curve = _curve;
            t = 0.0f;
        }

        /// <summary>
        /// Interpolates and returns the float between the given start and end.
        /// </summary>
        /// <returns></returns>
        public float Interpolate()
        {
            var interpolated = Mathf.Lerp(initial, target, curve.Evaluate(t));
            t += Time.deltaTime / rate;
            return interpolated;
        }

        private void CalculateTRate()
        {
            rate = Mathf.Abs(initial - target) / speed;
        }

        /// <summary>
        /// Sets the starting position for the interpolator.
        /// </summary>
        /// <param name="_start"></param>
        public void SetStart(float _start)
        {
            initial = _start;
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
