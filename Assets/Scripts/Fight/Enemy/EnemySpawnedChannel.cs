using System;
using UnityEngine;
using UnityEngine.Events;

namespace Fight
{
    [CreateAssetMenu(menuName = "WheelChairVR/Fight/Create EnemySpawnedChannel", fileName = "EnemySpawnedChannel",
        order = 0)]
    public class EnemySpawnedChannel : ScriptableObject
    {
        public EnemySpawnedEvent EnemySpawnedEvent = new EnemySpawnedEvent();

        public void InvokeSpawnNewEnemy(Enemy enemy)
        {
            EnemySpawnedEvent.Invoke(new EnemySpawnedEventArgs(enemy));
        }
    }

    [Serializable]
    public class EnemySpawnedEvent : UnityEvent<EnemySpawnedEventArgs>
    {
    }

    [Serializable]
    public class EnemySpawnedEventArgs
    {
        public Enemy Enemy;


        public EnemySpawnedEventArgs(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}