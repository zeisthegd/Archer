using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        [ReadOnly] public ObjectPooler EnemyPooler;

        [HorizontalLine]
        [Header("Spawn Settings")]
        public float MinDistanceToPlayer;
        public float MaxDistanceToPlayer;


        protected float _waitToSpawnTime = 0;
        protected bool _isSpawning = false;
        protected Vector2 _positionNearPlayer;

        private Enemy _boss;


        public event UnityAction BossSpawned;


        protected virtual void Update()
        {
            WaitToSpawnEnemy();
        }

        public virtual void LoadData()
        {
            if (EnemyPooler == null)
                CreateEnemyPool();

        }

        /// <summary>
        /// Create enemies pools.
        /// Connect the enemies' death events to the handler method.
        /// </summary>
        protected virtual void CreateEnemyPool()
        {
            EnemyPooler = Instantiate(EnemyPoolPrefab);
            EnemyPooler.ObjectToPool = MapData.EnemyPrefab.gameObject;
            EnemyPooler.Init();
            ConnectEnemyInPoolWithDeathEvent(EnemyPooler);
        }

        /// <summary>
        /// Spawn new enemies until the 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator SpawnRandomEnemies()
        {
            _isSpawning = true;

            EnemyData data = MapData.GetRandomEnemySpawnSettings();
            GameObject pooledObject = EnemyPooler.PullOneObject();
            SpawnOneEnemy(pooledObject, data);
            _waitToSpawnTime = MapData.EnemySpawningInterval;
            yield return null;

            _isSpawning = false;
        }

        /// <summary>
        /// Spawn a new enemy, load data if needed.
        /// Spawn near the player.
        /// </summary>
        public virtual void SpawnOneEnemy(GameObject pooledObject, EnemyData data)
        {
            Enemy enemy = pooledObject.GetComponent<Enemy>();
            enemy.Load(data);
            StartCoroutine(GetPositionNearPlayer());
            enemy.transform.position = _positionNearPlayer;
            enemy.gameObject.SetActive(true);
            LevelManager.Instance.Progress.CurrentValue += enemy.Data.ThreatLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ConnectEnemyInPoolWithDeathEvent(ObjectPooler pooler)
        {
            foreach (GameObject pooledbject in pooler.ObjectPool.PooledObjects)
            {
                pooledbject.GetComponent<Enemy>().Health.OnDeath += HandleEnemyDeath;
            }
        }

        /// <summary>
        /// Get an empty position near the player.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator GetPositionNearPlayer()
        {
            Vector2 playerPos = PlayerManager.Instance.Player.Position;
            _positionNearPlayer = Vector2.zero;
            float dst = 0;
            do
            {
                _positionNearPlayer = new Vector2(Randomizer.RandomNumber(playerPos.x - MaxDistanceToPlayer, playerPos.x + MaxDistanceToPlayer),
                Randomizer.RandomNumber(playerPos.y - MaxDistanceToPlayer, playerPos.y + MaxDistanceToPlayer));
                dst = Vector3.Distance(_positionNearPlayer, playerPos);
                yield return null;
            }
            while (dst < MinDistanceToPlayer || dst > MaxDistanceToPlayer);
            yield return _positionNearPlayer;
        }


        /// <summary>
        /// When an enemy dies, delay a bit before spawning new ones.
        /// </summary>
        public virtual void HandleEnemyDeath(Character character)
        {
            Enemy enemy = character.GetComponent<Enemy>();

            PlayerManager.Instance.Player.Experience.CurrentValue += enemy.Data.ThreatLevel;
        }

        protected virtual void WaitToSpawnEnemy()
        {
            if (LevelManager.Instance.IsPlaying)
            {
                if (_waitToSpawnTime > 0)
                    _waitToSpawnTime -= Time.deltaTime;
                if (_waitToSpawnTime <= 0 && !_isSpawning)
                {
                    _waitToSpawnTime = 0;
                    StartCoroutine(SpawnRandomEnemies());
                }
            }
        }

        public Enemy Boss { get => _boss; }
    }
}

