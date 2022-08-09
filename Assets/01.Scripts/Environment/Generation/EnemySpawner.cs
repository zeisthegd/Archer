using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Map Data")]
        public MapData MapData;

        [Header("Pooling Settings")]
        public ObjectPooler EnemyPoolPrefab;
        [ReadOnly] public List<ObjectPooler> ObjectPoolers;

        [HorizontalLine]
        [Header("Spawn Settings")]
        public float MinDistanceToPlayer;
        public float MaxDistanceToPlayer;
        public float TimeTillSpawnNewEnemies = 2;


        protected float _waitToSpawnTime = 0;
        protected bool _isSpawning = false;

        protected virtual void Update()
        {
            WaitToSpawnEnemy();
            HandleMaxLevelThreatIncreased();
        }

        public virtual void LoadData()
        {
            CreateEnemyPools();
        }

        /// <summary>
        /// Create enemies pools.
        /// Connect the enemies' death events to the handler method.
        /// </summary>
        protected virtual void CreateEnemyPools()
        {
            ObjectPoolers = new List<ObjectPooler>();
            foreach (EnemySpawnSettings spawnSettings in MapData.SpawnSettings)
            {
                ObjectPooler enemyPool = Instantiate(EnemyPoolPrefab);
                enemyPool.ObjectToPool = spawnSettings.Prefab.gameObject;
                enemyPool.Init();
                ObjectPoolers.Add(enemyPool);
                ConnectEnemyInPoolWithDeathEvent(enemyPool);
            }
        }

        /// <summary>
        /// Spawn new enemies until the 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator SpawnRandomEnemies()
        {
            if (MapData.SpawnSettings.Length > 0)
            {
                _isSpawning = true;
                while (LevelManager.Instance.CurrentThreatLevel < LevelManager.Instance.MaxThreatLevel)
                {
                    EnemySpawnSettings settings = MapData.GetRandomEnemySpawnSettings();
                    EnemyData randomEnemyData = settings.GetRandomEnemyData();
                    foreach (ObjectPooler pooler in ObjectPoolers)
                    {
                        if (pooler.ObjectToPool.gameObject == settings.Prefab.gameObject)
                        {
                            GameObject pooledObject = pooler.PullOneObject();
                            SpawnOneEnemy(pooledObject, randomEnemyData);
                            break;
                        }
                    }
                    yield return null;
                }
            }
            else
                Debug.LogWarning("Not enemy spawn settings inserted");
            _isSpawning = false;
        }

        /// <summary>
        /// Spawn a new enemy, load data if needed.
        /// Spawn near the player.
        /// </summary>
        public virtual void SpawnOneEnemy(GameObject pooledObject, EnemyData data)
        {
            Enemy enemy = pooledObject.GetComponent<Enemy>();
            enemy.AIBrain.Enabled = true;
            enemy.LoadEnemy(data);
            enemy.transform.position = GetPositionNearPlayer();
            LevelManager.Instance.CurrentThreatLevel += data.ThreatLevel;
            enemy.gameObject.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ConnectEnemyInPoolWithDeathEvent(ObjectPooler pooler)
        {
            foreach (GameObject pooledbject in pooler.ObjectPool.PooledObjects)
            {
                pooledbject.GetComponent<Enemy>().Health.OnDeath += HandleEnemyDeath;
                pooledbject.GetComponent<Enemy>().Health.OnDeath += LevelManager.Instance.LootDropManager.HandleEnemyDeath;
            }
        }

        /// <summary>
        /// Get an empty position near the player.
        /// </summary>
        /// <returns></returns>
        protected virtual Vector3 GetPositionNearPlayer()
        {
            Vector3 randomPosNearPlayer;
            float dst = 0;
            do
            {
                randomPosNearPlayer = LevelManager.Instance.LevelGenerator.GetRandomEmptyPosition();
                dst = Vector3.Distance(randomPosNearPlayer, PlayerManager.Instance.Player.Position);
            }
            while (dst < MinDistanceToPlayer || dst > MaxDistanceToPlayer);
            return randomPosNearPlayer;
        }


        /// <summary>
        /// When an enemy dies, delay a bit before spawning new ones.
        /// </summary>
        public virtual void HandleEnemyDeath(Character character)
        {
            Enemy enemy = character.GetComponent<Enemy>();
            LevelManager.Instance.CurrentThreatLevel -= enemy.Data.ThreatLevel;
            StartWaitToSpawnEnemyCounter();
        }

        public virtual void HandleMaxLevelThreatIncreased()
        {
            if (LevelManager.Instance.MaxThreatLevel - LevelManager.Instance.CurrentThreatLevel > 1
                    && _isSpawning == false && _waitToSpawnTime <= 0)
                StartWaitToSpawnEnemyCounter();
        }

        public virtual void StartWaitToSpawnEnemyCounter()
        {
            _waitToSpawnTime = TimeTillSpawnNewEnemies;
        }

        protected virtual void WaitToSpawnEnemy()
        {
            if (_waitToSpawnTime > 0)
                _waitToSpawnTime -= Time.deltaTime;
            if (_waitToSpawnTime < 0)
            {
                _waitToSpawnTime = 0;
                StartCoroutine(SpawnRandomEnemies());
            }
        }
    }
}

