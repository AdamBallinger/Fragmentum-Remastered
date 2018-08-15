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

        public FloatInterpolator(float _start, float _end, float _speed, AnimationCurve _curve)
        {
            initial = _start;
            target = _end;
            speed = _speed;
            curve = _curve;
            t = 0.0f;
        }

        /// <summary>
        /// Interpolates and returns the float between the given start and end.
        /// </summary>
        /// <returns></returns>
        public float Interpolate()
        {
            t += Time.deltaTime / speed;
            return Mathf.Lerp(initial, target, curve.Evaluate(t));
        }

        /// <summary>
        /// Sets the starting position for the interpolator.
        /// </summary>
        /// <param name="_start"></param>
        public void SetStart(float _start)
        {
            initial = _start;
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