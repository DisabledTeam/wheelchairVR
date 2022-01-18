using UnityEngine;
using UnityEngine.AI;

namespace Fight
{
    public class EnemyFollowState : EnemyState
    {
        [Header("Settings")]
        [SerializeField] private bool stopOnRange;
        [Header("Links")]
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private EnemyView enemyView;

        public override void Enter()
        {
            if (PlayerTargetMarker.GetInstance() == null) GoToWait();
            else if (enemyView) enemyView.Walk();
        }

        public override void Leave()
        {
            if (stopOnRange)
            {
                navMeshAgent.isStopped = true;
                if (enemyView) enemyView.UnWalk();
            }
        }

        public override void UpdateState()
        {
            if (enemy.Player)
            {
                navMeshAgent.SetDestination(enemy.Player.FollowPoint.position);
                navMeshAgent.isStopped = false;
            }
            else
            {
                GoToWait();
                return;
            }

            if (CheckAttackRange()) GoToAttack();
        }
    }
}