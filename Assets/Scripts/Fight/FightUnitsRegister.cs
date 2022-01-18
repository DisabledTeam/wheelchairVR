using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class FightUnitsRegister : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private EnemySpawnedChannel enemySpawnedChannel;

        [Header("Monitoring")]
        [SerializeField] private PlayerTargetMarker playerTargetMarker;
        [SerializeField] private List<Enemy> enemyList;


        public List<Enemy> AllEnemy => enemyList;
        public PlayerTargetMarker Player => playerTargetMarker;

        public void RegisterEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
            enemy.EnemyDied.AddListener(OnEnemyDied);
        }

        public void UnRegisterEnemy(Enemy enemy)
        {
            enemy.EnemyDied.RemoveListener(OnEnemyDied);
            enemyList.Remove(enemy);
        }

        private void OnEnemyDied(Enemy arg0)
        {
            UnRegisterEnemy(arg0);
        }

        private void OnEnable()
        {
            PlayerTargetMarker.PlayerTargetMarkerSpawned.AddListener(OnPlayerSpawned);
            enemySpawnedChannel.EnemySpawnedEvent.AddListener(OnEnemySpawned);
        }

        private void OnDisable()
        {
            PlayerTargetMarker.PlayerTargetMarkerSpawned.RemoveListener(OnPlayerSpawned);
            enemySpawnedChannel.EnemySpawnedEvent.RemoveListener(OnEnemySpawned);
        }

        private void OnEnemySpawned(EnemySpawnedEventArgs arg0)
        {
            RegisterEnemy(arg0.Enemy);
        }

        private void OnPlayerSpawned()
        {
            playerTargetMarker = PlayerTargetMarker.GetInstance();
        }
    }
}