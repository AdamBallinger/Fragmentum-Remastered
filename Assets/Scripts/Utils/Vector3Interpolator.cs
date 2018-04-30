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

        private bool speedAsRate;

        /// <summary>
        /// Linearly interpolate between a given start and end vector3.
        /// </summary>
        /// <param name="_start">Where to start interpolating from.</param>
        /// <param name="_end">Where to interpolate to.</param>
        /// <param name="_speed">Speed to interpolate at.</param>
        /// <param name="_curve">Smoothing curve to apply to the interpolation.</param>
        /// <param name="_speedAsRate">If true, speed will be the time in seconds it takes to interpolate.</param>
        public Vector3Interpolator(Vector3 _start, Vector3 _end, float _speed, AnimationCurve _curve, bool _speedAsRate = false)
        {
            initialPosition = _start;
            targetPosition = _end;
            speed = _speed;
            curve = _curve;
            speedAsRate = _speedAsRate;
            t = 0.0f;

            CalculateTRate();
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

        /// <summary>
        /// Set a custom rate in which the interpolator steps each frame. By default, the interpolator rate is calculated
        /// based on the distance between the given start/end vectors. This needs to be called each time the interpolator
        /// start is changed.
        /// </summary>
        /// <param name="_rate"></param>
        public void SetCustomRate(float _rate)
        {
            rate = _rate;
        }

        private void CalculateTRate()
        {
            if(speedAsRate)
            {
                rate = speed;
            }
            else
            {
                rate = Vector3.Distance(initialPosition, targetPosition) / speed;
            }        
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
