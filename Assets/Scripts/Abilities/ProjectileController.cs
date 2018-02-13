using Scripts.Extensions;
using UnityEngine;

namespace Scripts.Abilities
{
    public class ProjectileController : MonoBehaviour
    {
        public Vector3 Direction { get; set; }

        public LayerMask CollisionMask { get; set; }

        [SerializeField]
        private float speed = 1.0f;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position += Direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider _col)
        {
            if(CollisionMask.Contains(_col.gameObject.layer))
            {
                gameObject.SetActive(false);
            }   
        }
    }
}
