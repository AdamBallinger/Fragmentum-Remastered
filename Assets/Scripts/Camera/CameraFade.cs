using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Camera
{
    public class CameraFade : MonoBehaviour
    {
        [SerializeField]
        private Image fadeImage;

        [SerializeField]
        private AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

        private FloatInterpolator alphaInterpolator;

        /// <summary>
        /// Fade into a black screen in the specified duration (seconds).
        /// </summary>
        /// <param name="_duration"></param>
        public void FadeIn(float _duration)
        {
            alphaInterpolator = new FloatInterpolator(0.0f, 1.0f, _duration, curve);
        }

        /// <summary>
        /// Fade out of a black screen in the specified duration (seconds).
        /// </summary>
        /// <param name="_duration"></param>
        public void FadeOut(float _duration)
        {
            var col = fadeImage.color;
            col.a = 1.0f;
            fadeImage.color = col;

            alphaInterpolator = new FloatInterpolator(1.0f, 0.0f, _duration, curve);
        }

        private void Update()
        {
            if (alphaInterpolator == null || alphaInterpolator.HasFinished())
            {
                return;
            }

            var col = fadeImage.color;

            col.a = alphaInterpolator.Interpolate();

            fadeImage.color = col;
        }
    }
}