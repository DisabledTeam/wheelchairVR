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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Health>(out var health))
            {
                health.GetDamage(damage);
                if (destroyOnDamage) Destroy(gameObject);
            }

            if (destroyOnCollide) Destroy(gameObject);
        }
    }
}