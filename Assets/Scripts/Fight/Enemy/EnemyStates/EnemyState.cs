using System;
using UnityEngine;

namespace Fight
{
    public abstract class EnemyState : MonoBehaviour
    {
        protected Enemy enemy;
        protected EnemyStateMachine stateMachine;

        public void Initialize(Enemy enemy, EnemyStateMachine stateMachine)
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Leave()
        {
        }


        public virtual void UpdateState()
        {
        }

        protected bool CheckAttackRange()
        {
            return Vector3.Distance(enemy.Player.FollowPoint.position, transform.position) <= enemy.AttackRange;
        }

        protected void GoToWait()
        {
            stateMachine.ChangeState(typeof(EnemyWaitPlayerState));
        }

        protected void GoToFollow()
        {
            stateMachine.ChangeState(typeof(EnemyFollowState));
        }

        protected void GoToAttack()
        {
            stateMachine.ChangeState(typeof(EnemyAttackState));
        }
    }
}