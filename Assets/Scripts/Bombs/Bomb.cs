using System;
using Units;
using UnityEngine;

namespace Bombs
{
    public abstract class Bomb : MonoBehaviour
    {
        [SerializeField] private float ExplosionForce = 5f;
        [SerializeField] protected float ExplosionRadious = 5f;
        [SerializeField] private GameObject explosionEffectPrefab;
        [SerializeField] private float gravityPull = 35f;
        [SerializeField] private int bombDamage;
        [SerializeField] private AudioClip explosionAudioClip;

        private Rigidbody rb;
        private LayerMask layerMask;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            layerMask = LayerMask.GetMask("Player", "Enemy", "Ammo");
            Initialize();
        }

        private void Update()
        {
            Refresh();
        }

        private void FixedUpdate()
        {
            RefreshGravity();
        }

        protected abstract void Initialize();
        protected abstract void Refresh();

        protected virtual void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadious, layerMask);

            foreach (Collider colider in colliders)
            {
                Rigidbody rb = colider.GetComponent<Rigidbody>();
                Unit unit = colider.GetComponent<Unit>();
                if (unit!=null)
                {
                    unit.TakeDamage(bombDamage);
                }

                rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadious, 1f, ForceMode.Impulse);
            }

            AudioSource.PlayClipAtPoint(explosionAudioClip, transform.position, 1f);
            Destroy(this.gameObject);
            PlayExplosionParticle();
        }
        
        private void RefreshGravity()
        {
            Vector3 myVelocity = rb.velocity;
            myVelocity.y -= gravityPull * Time.deltaTime;
            rb.velocity = myVelocity;
        }

        private void PlayExplosionParticle()
        {
            GameObject explosionEffectObject = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffectObject, 1f);
        }

      
    }
}