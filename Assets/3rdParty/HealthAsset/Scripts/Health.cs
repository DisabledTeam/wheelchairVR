using System;
using Fight;
using UnityEngine;
using UnityEngine.Events;

namespace HealthBar
{
    [Serializable]
    public class DamageGottenEvent : UnityEvent<Health, float>
    {
    }

    [Serializable]
    public class DieEvent : UnityEvent<Health>
    {
    }

    [RequireComponent(typeof(DamagePopUpper))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private Team team = Team.Enemy;
        public bool needDestroyOnDie = true;
        public float DestroyDelay = 2f;
        [HideInInspector] public bool needHealthBar = true;
        [HideInInspector] public HealthBar healthBar;

        [HideInInspector] public bool needDamagePopUp = true;
        [HideInInspector] public DamagePopUpper damagePopUpper;

        [SerializeField] private float maxHealthPoints;
        [SerializeField] private float currentHealthPoints;


        [SerializeField] private bool idDead;


        public Team Team => team;

        public DamageGottenEvent damageGottenEvent = new DamageGottenEvent();
        public DieEvent dieEvent = new DieEvent();

        public float MaxHealthPoints
        {
            get => maxHealthPoints;
            private set => maxHealthPoints = value;
        }

        public float CurrentHealthPoints
        {
            get => currentHealthPoints;
            private set => currentHealthPoints = value;
        }


        public void GetDamage(float damage, bool isCritical = false)
        {
            damageGottenEvent.Invoke(this, damage);
            if (CurrentHealthPoints - damage <= 0)
            {
                CurrentHealthPoints = 0;
                Die();
            }
            else
            {
                CurrentHealthPoints -= damage;
            }
            
            if (needHealthBar) ChangeHealthBar();
            if (needDamagePopUp) PopUpDamage(damage, isCritical);
        }

        //unity events - shit 
        public void GetDamage(float damage)
        {
            GetDamage(damage, false);
        }

        public void Die()
        {
            if (idDead) return;
            idDead = true;
            dieEvent.Invoke(this);
            if (needDestroyOnDie) Destroy(gameObject, DestroyDelay);
        }


        private void Awake()
        {
            damagePopUpper = GetComponent<DamagePopUpper>();
        }

        private void ChangeHealthBar()
        {
            if (healthBar) healthBar.HandleHealthChanged(currentHealthPoints / maxHealthPoints);
            else
            {
                throw new NullReferenceException("Health Bar didn't set");
            }
        }

        private void PopUpDamage(float damage, bool isCritical)
        {
            if (damagePopUpper)
            {
                damagePopUpper.SpawnFloatingDamage(damage, isCritical);
            }
            else
            {
                throw new NullReferenceException("damagePopUpper didn't set");
            }
        }
    }
}