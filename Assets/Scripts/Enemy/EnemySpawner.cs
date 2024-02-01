using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Spawner Preferences")] 
        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private bool randomSpawnDelay;
        [SerializeField, Range(0, 10f)] private float spawnDelayRange;
        [SerializeField, Range(0, 100)] private int spawnChanceRange;

        private float _spawnDelay;
        
        // Start is called before the first frame update
        void Start()
        {
            var spawnChanceRand = Random.Range(0, 100);
            if (spawnChanceRand <= spawnChanceRange)
            {
                _spawnDelay = randomSpawnDelay ? Random.Range(0, 10f) : spawnDelayRange;
                StartCoroutine(SpawnEnemy());
            }
            
        }

        private IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(_spawnDelay);
            Instantiate(enemyPrefab);
        }
    }
}
