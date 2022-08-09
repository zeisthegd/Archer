using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public LevelManager LevelManager;
        public InputReader InputReader;
        public PlayerManager PlayerManager;

        public bool AutoStart = false;

        public void Start()
        {
            if (AutoStart)
                StartGame();
        }

        public void StartGame()
        {
            PlayerManager.SpawnPlayer();
            InputReader.EnableGameplayInput();
        }
    }
}