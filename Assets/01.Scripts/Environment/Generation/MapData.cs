using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Environment/LevelGeneration/MapData")]
    public class MapData : ScriptableObject
    {
        public string Name;
        [Header("Enemies")]
        public Enemy EnemyPrefab;
        public List<EnemyData> EnemyDatas;
        public float EnemySpawningInterval = 1.5F;


        public virtual EnemyData GetRandomEnemySpawnSettings()
        {
            if (EnemyDatas.Count > 0)
                return EnemyDatas[Randomizer.RandomNumber(0, EnemyDatas.Count)];
            return null;
        }
    }
}

