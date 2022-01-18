using System;
using HealthBar;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Fight
{
    public class Enemy : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackRange;

        [Header("Links")]
        [SerializeField] private Health health;
        [SerializeField] private EnemyStateMachine stateMachine;


        [Header("Monitoring")]
        [SerializeField] private PlayerTargetMarker player;
        [SerializeField] private bool isDead;

        public bool IsDead => isDead;
        public PlayerTargetMarker Player => player;

        public float AttackCooldown => attackCooldown;
        public float AttackDamage => attackDamage;
        public float AttackRange => attackRange;

        public UnityEvent<Enemy> EnemyDied;

        public void SetPlayerMarker(PlayerTargetMarker player)
        {
            this.player = player;
        }

        private void Start()
        {
            stateMachine.ChangeState(typeof(EnemyWaitPlayerState));
        }


        private void OnEnable()
        {
            health.dieEvent.AddListener(OnDie);
        }


        private void OnDisable()
        {
            health.dieEvent.RemoveListener(OnDie);
        }

        private void OnDie(Health arg0)
        {
            stateMachine.ChangeState(typeof(EnemyDiedState));
            EnemyDied.Invoke(this);
            isDead = true;
        }

    }
}