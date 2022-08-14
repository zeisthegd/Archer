using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;
using NaughtyAttributes;


namespace Penwyn.Game
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        [Header("Map Datas")]
        public List<MapData> MapDatas;


        [Header("Sub-components")]
        public EnemySpawner EnemySpawner;
        public LootDropManager LootDropManager;

        [Header("Threat Level")]
        public float CurrentThreatLevel;
        protected float _maxThreatLevel;
        protected float _progress;
        protected MapData _mapData;

        /// <summary>
        /// Generate the level and spawn the enemies.
        /// </summary>
        public virtual void LoadLevel()
        {
            ChangeToRandomData();
        }

        /// <summary>
        /// Change the level's data to a random one of the list.
        /// </summary>
        public virtual void ChangeToRandomData()
        {
            MapData randomData = MapDatas[Randomizer.RandomNumber(0, MapDatas.Count)];
            _mapData = Instantiate(randomData);

            EnemySpawner.MapData = _mapData;
            LootDropManager.MapData = _mapData;

            EnemySpawner.LoadData();

            CurrentThreatLevel = 0;
            _progress = 0;
            _maxThreatLevel = _mapData.StartingThreatLevel;
        }

        public float MaxThreatLevel { get => _maxThreatLevel; }
        public float Progress { get => _progress; }
    }
}
