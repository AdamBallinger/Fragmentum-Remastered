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

        [SerializeField]
        private bool delayedDeactivation = false;

        [SerializeField]
        private float delay = 0.0f;

        [SerializeField]
        private ParticleSystem contactParticleSystem = null;

        private bool deactivating;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            deactivating = false;
            GetComponent<ParticleSystem>()?.Play(false);
        }

        private void Update()
        {
            _transform.position += Direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider _col)
        {
            if(CollisionMask.Contains(_col.gameObject.layer) && !deactivating)
            {          
                if(!delayedDeactivation)
                {
                    Deactivate();
                }
                else
                {
                    GetComponent<ParticleSystem>()?.Stop();
                    deactivating = true;
                    Invoke("Deactivate", delay);
                }

                contactParticleSystem.Play();
            }   
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
