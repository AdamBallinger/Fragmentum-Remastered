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
        private float maxLifeTime = 1.0f;

        private float currentLifeTime = 0.0f;

        [SerializeField]
        private ParticleSystem projectileParticleSystem = null;

        [SerializeField]
        private ParticleSystem contactParticleSystem = null;

        private bool deactivating;

        private Transform _transform;

        private void OnEnable()
        {
            if(_transform == null)
            {
                _transform = transform;
            }

            deactivating = false;
            currentLifeTime = 0.0f;

            if(projectileParticleSystem != null)
            {
                projectileParticleSystem.Play();
            }
        }

        private void Update()
        {
            if(currentLifeTime >= maxLifeTime)
            {
                Deactivate();
            }

            _transform.position += Direction * (speed * Time.deltaTime);

            currentLifeTime += Time.deltaTime;
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
                    if(projectileParticleSystem != null)
                    {
                        projectileParticleSystem.Stop();
                    }

                    deactivating = true;
                    Invoke("Deactivate", delay);
                }

                if(contactParticleSystem != null)
                {
                    contactParticleSystem.Play();
                }
            }   
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
