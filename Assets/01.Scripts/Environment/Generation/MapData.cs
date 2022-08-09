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
        public EnemySpawnSettings[] SpawnSettings;
        public float StartingThreatLevel = 3;
        public float ThreatLevelIncrementPerSecond = 0.1F;

        [HorizontalLine(1, EColor.Green)]
        [Header("Environment")]
        public int Width = 50;
        public int Height = 50;
        [Range(0, 100)] public int FillPercent = 50;
        public int ResmoothWallTimes = 4;
        public int MinNeighborWalls = 4;
        public string Seed;
        public bool UseRandomSeed;
        [Header("Tilemap")]
        public TileBase GroundTile;
        public TileBase WallTile;


        [HorizontalLine(1, EColor.Green)]
        [Header("Drop")]
        public CoinDrop LesserMoneyDrop;
        public CoinDrop GreaterMoneyDrop;
        public CoinDrop EliteMoneyDrop;


        public virtual EnemySpawnSettings GetRandomEnemySpawnSettings()
        {
            if (SpawnSettings.Length > 0)
                return SpawnSettings[Randomizer.RandomNumber(0, SpawnSettings.Length)];
            return new EnemySpawnSettings();
        }
    }

    [System.Serializable]
    public struct EnemySpawnSettings
    {
        public string Name;
        public Enemy Prefab;
        public EnemyData[] Datas;

        public EnemyData GetRandomEnemyData()
        {
            if (Datas.Length > 0)
                return Datas[Randomizer.RandomNumber(0, Datas.Length)];
            Debug.Log("No enemy data inserted!");
            return null;
        }
    }

    [System.Serializable]
    public struct CoinDrop
    {
        public Sprite CoinSprite;
        public int MoneyAmount;
        public int HealAmount;
    }
}

