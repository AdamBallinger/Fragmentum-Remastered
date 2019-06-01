using UnityEngine;

namespace Scripts.Camera
{
    public class CameraBillboard : MonoBehaviour
    {
        private new UnityEngine.Camera camera;

        private new Transform transform;
        
        private void Start()
        {
            camera = UnityEngine.Camera.main;
            transform = GetComponent<Transform>();
        }
        
        private void LateUpdate()
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
    }
}
