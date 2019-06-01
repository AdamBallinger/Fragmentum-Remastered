using UnityEngine;

namespace Scripts.Utils
{
    public class ScaleOverTime : MonoBehaviour
    {
        private new Transform transform;

        [SerializeField]
        private float scaleSpeed = 1.0f;

        [SerializeField]
        private float minScale = 0.1f;
        [SerializeField]
        private float maxScale = 1.0f;

        private bool isReversing = false;
        
        private void Start()
        {
            transform = GetComponent<Transform>();
        }
        
        private void Update()
        {
            var scale = transform.localScale;
            var scaleDelta = Vector3.one * scaleSpeed * Time.deltaTime;
            
            if (isReversing)
            {
                scale -= scaleDelta;
                
                if (scale.x < minScale)
                {
                    isReversing = false;
                }
            }
            else
            {
                scale += scaleDelta;
                
                if (scale.x > maxScale)
                {
                    isReversing = true;
                }
            }

            transform.localScale = scale;
        }
    }
}
