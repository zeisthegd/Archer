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

        private FloatValue _progress;
        protected MapData _mapData;
        private StateMachine<LevelState> _state = new StateMachine<LevelState>(LevelState.Idle);

        public event UnityAction Loaded;


        #region Level Loading###################################################

        /// <summary>
        /// Generate the level and spawn the enemies.
        /// </summary>
        public virtual void LoadLevel()
        {
            ChangeToRandomData();
            _state.Change(LevelState.Playing);
            Loaded?.Invoke();
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
            _progress = new FloatValue(100);
        }

        #endregion

        #region Events
        public void OnProgressReachedMax()
        {

        }

        #endregion

        public FloatValue Progress { get => _progress; }
        public StateMachine<LevelState> State { get => _state; }
        public bool IsPlaying => _state.Is(LevelState.Playing);
    }

    public enum LevelState
    {
        Idle,
        Playing
    }
}
