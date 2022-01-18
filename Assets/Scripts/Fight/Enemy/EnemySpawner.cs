using InspectorButton;
using UnityEngine;

namespace Fight
{
    [InspectorButtonClass]
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private EnemySpawnedChannel enemySpawnedChannel;


        public void SpawnEnemy(Vector3 position)
        {
            var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemySpawnedChannel.InvokeSpawnNewEnemy(newEnemy);
        }

        [InspectorButton("SpawnEnemy")]
        public void SpawnEnemy()
        {
            SpawnEnemy(transform.position);
        }
    }
}