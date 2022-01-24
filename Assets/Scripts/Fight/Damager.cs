using System;
using HealthBar;
using UnityEngine;

namespace Fight
{
    public enum Team
    {
        Player,
        Enemy
    }

    public class Damager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Team attackerTeam = Team.Player;
        [SerializeField] private float damage;
        [SerializeField] private bool destroyOnDamage;
        [SerializeField] private bool destroyOnCollide;
        [SerializeField] private ParticleSystem destroyParticles;


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Health>(out var health))
            {
                if (health.Team == attackerTeam)
                    return;
                else
                    health.GetDamage(damage);
                if (destroyOnDamage)
                {
                    DestroyView();
                }
            }

            if (destroyOnCollide)
            {
                if (other.gameObject.TryGetComponent<Damager>(out var damager))
                {
                    if (damager.attackerTeam == attackerTeam) return;
                }

                DestroyView();
            }
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