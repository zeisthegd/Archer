using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
    {
        [Header("Player")]
        public Player PlayerToSpawn;
        public Player ExistedPlayer;

        private Player _player;


        public static event UnityAction PlayerSpawned;
        /// <summary>
        /// Spawn player if they are not existed.
        /// </summary>
        public virtual void SpawnPlayer()
        {
            if (ExistedPlayer != null)
                _player = ExistedPlayer;
            else if (PlayerToSpawn != null)
                _player = Instantiate(PlayerToSpawn);
            PlayerSpawned?.Invoke();
        }


        public virtual void MovePlayerTo(Vector2 position)
        {
            _player.transform.position = position;
        }

        public Player Player { get => _player; }
    }
}