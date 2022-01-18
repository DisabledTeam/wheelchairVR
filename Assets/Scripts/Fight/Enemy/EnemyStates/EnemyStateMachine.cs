using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyState currentState;
        [SerializeField] private List<EnemyState> states;
        [SerializeField] private Enemy enemy;

        private Dictionary<Type, EnemyState> _enemyStates;

        private void Awake()
        {
            _enemyStates = new Dictionary<Type, EnemyState>();
            foreach (var enemyState in states)
            {
                _enemyStates.Add(enemyState.GetType(), enemyState);
                enemyState.Initialize(enemy, this);
            }
        }


        private void Update()
        {
            if (currentState) currentState.UpdateState();
        }


        public void ChangeState(Type stateType)
        {
            if (currentState) currentState.Leave();
            currentState = _enemyStates[stateType];
            currentState.Enter();
        }
    }
}