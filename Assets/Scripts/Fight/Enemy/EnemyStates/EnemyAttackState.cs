using HealthBar;
using InspectorButton;
using UnityEngine;
using UnityEngine.Events;

namespace Fight
{
    [InspectorButtonClass]
    public class EnemyAttackState : EnemyState
    {
        public UnityEvent<Enemy> EnemyAttacked;
        [SerializeField] private EnemyView enemyView;

        private float _lastCooldownTime;

        [Header("Monitoring")]
        [SerializeField] private bool playerInRange;

        public override void Enter()
        {
            enemy.Player.Health.dieEvent.AddListener(OnPlayerDead);
        }

        public override void Leave()
        {
            playerInRange = false;
            enemyView.DropAttack();
            if (enemy.Player) enemy.Player.Health.dieEvent.RemoveListener(OnPlayerDead);
        }

        public override void UpdateState()
        {
            if (!CheckAttackRange())
            {
                playerInRange = false;

                GoToFollow();
                return;
            }

            playerInRange = true;

            if (Time.time - _lastCooldownTime > enemy.AttackCooldown)
            {
                TriggerAttack();
                _lastCooldownTime = Time.time;
            }
        }

        private void OnPlayerDead(Health arg0)
        {
            GoToWait();
        }


        [InspectorButton("AttackAnimationEvent")]
        private void TriggerAttack()
        {
            if (enemyView) enemyView.TriggerAttack(Attack);
        }

        private void Attack()
        {
            Debug.Log("Attack");
            enemy.Player.Health.GetDamage(enemy.AttackDamage);
            EnemyAttacked.Invoke(enemy);
        }

        private void OnDrawGizmos()
        {
            if (playerInRange)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, enemy.AttackRange);
            }
        }
    }
}