using System;
using HealthBar;
using InspectorButton;
using UnityEngine;

namespace Fight
{
    [InspectorButtonClass]
    public class EnemyView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float timeToDestroyOnDead;
        [Header("Links")]
        [SerializeField] private Animator animator;
        [SerializeField] private Enemy enemy;
        [SerializeField] private Health health;

        private static readonly int DamageTrigger = Animator.StringToHash("Damage");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int DeadBool = Animator.StringToHash("Dead");
        private static readonly int WalkBool = Animator.StringToHash("Walk");
        private static readonly int DiedTrigger = Animator.StringToHash("Died");
        private static readonly int DropAttackTrigger = Animator.StringToHash("DropAttack");

        private Action attackToInvoke;
        private bool _attacking;

        private void OnEnable()
        {
            enemy.EnemyDied.AddListener(OnEnemyDied);
            health.damageGottenEvent.AddListener(OnDamage);
        }

        private void OnDisable()
        {
            enemy.EnemyDied.RemoveListener(OnEnemyDied);
            health.damageGottenEvent.RemoveListener(OnDamage);
        }

        public void Walk()
        {
            animator.SetBool(WalkBool,true);
        }
        
        public void UnWalk()
        {
            animator.SetBool(WalkBool,false);
        }
        
        // инвокается из анимации
        public void AttackAnimationEvent()
        {
            Debug.Log("AttackAnimationEvent");
            attackToInvoke?.Invoke();
            attackToInvoke = null;
            _attacking = false;
        }
        
        public bool TriggerAttack(Action attack)
        {
            if(_attacking) return false;
            Debug.Log("TriggerAttack");
            attackToInvoke = attack;
            animator.SetTrigger(AttackTrigger);
            _attacking = true;
            return true;
        }


        [InspectorButton("DropAttack")]
        public void DropAttack()
        {
            attackToInvoke = null;
            animator.SetTrigger(DropAttackTrigger); 
            animator.ResetTrigger(AttackTrigger);
            _attacking = false;
        }
        
        private void OnDamage(Health arg0, float arg1)
        {
            animator.SetTrigger(DamageTrigger);
        }

        private void OnEnemyDied(Enemy arg0)
        {
            animator.SetTrigger(DiedTrigger);
            animator.SetBool(DeadBool, true);
            Destroy(gameObject, timeToDestroyOnDead);
        }
    }
}