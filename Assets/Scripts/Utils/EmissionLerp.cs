using UnityEngine;

namespace Scripts.Utils
{
    public class EmissionLerp : MonoBehaviour
    {
        public new Renderer renderer;
        public Color start, end;

        public float speed = 1.0f;

        public AnimationCurve curve;

        private float t;

        private bool reversing;

        private MaterialPropertyBlock materialPB;

        private void Start()
        {
            materialPB = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if(!reversing)
            {
                if(t >= 1.0f)
                {
                    t = 1.0f;
                    reversing = true;
                    return;
                }

                t += speed * Time.deltaTime;
            }

            if(reversing)
            {
                if(t <= 0.0f)
                {
                    t = 0.0f;
                    reversing = false;
                }

                t -= speed * Time.deltaTime;
            }

            materialPB.SetColor("_EmissionColor", Color.Lerp(start, end, curve.Evaluate(t)));
            renderer.SetPropertyBlock(materialPB);
        }
    }
}
