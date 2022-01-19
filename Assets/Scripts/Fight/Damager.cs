using System;
using HealthBar;
using UnityEngine;

namespace Fight
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private bool destroyOnDamage;
        [SerializeField] private bool destroyOnCollide;
        [SerializeField] private ParticleSystem destroyParticles;


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Health>(out var health))
            {
                health.GetDamage(damage);
                if (destroyOnDamage) DestroyView();
            }

            if (destroyOnCollide) DestroyView();
        }


        private void DestroyView()
        {
            if (destroyParticles)
            {
                var particle = Instantiate(destroyParticles);
                particle.transform.position = transform.position;
            }

            Destroy(gameObject);
        }
    }
}