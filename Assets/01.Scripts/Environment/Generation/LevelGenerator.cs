using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class LevelGenerator : MonoBehaviour
    {
        public LevelBuilder LevelBuilder;

        protected int[,] _map;
        protected string _seed;
        [Expandable] public MapData MapData;

        [Button("Generate Level", EButtonEnableMode.Playmode)]
        public virtual void GenerateLevel()
        {
            _map = new int[MapData.Width, MapData.Height];
            FillWalls();
            SmoothWalls();
            Debug.Log("Level Generated");
            LevelBuilder.BuildMap(this);
            PlayerManager.Instance.MovePlayerTo(GetRandomEmptyPosition());
        }

        public virtual void FillWalls()
        {
            _seed = MapData.Seed;
            if (MapData.UseRandomSeed)
                _seed = Randomizer.RandomString(10);
            System.Random rndNumber = new System.Random(_seed.GetHashCode());
            for (int x = 0; x < MapData.Width; x++)
            {
                for (int y = 0; y < MapData.Height; y++)
                {
                    if (x == 0 || y == 0 || x == MapData.Width - 1 || y == MapData.Height - 1)
                        _map[x, y] = 1;
                    else
                        _map[x, y] = (rndNumber.Next(0, 100) < MapData.FillPercent) ? 1 : 0;
                }
            }

        }

        public virtual void SmoothWalls()
        {
            for (int i = 0; i < MapData.ResmoothWallTimes; i++)
            {
                ConnectMapWalls();
            }
        }

        public virtual void ConnectMapWalls()
        {
            for (int x = 0; x < MapData.Width; x++)
            {
                for (int y = 0; y < MapData.Height; y++)
                {
                    int neighborWallsCount = GetNeighborWallsCount(x, y);
                    if (neighborWallsCount > MapData.MinNeighborWalls)
                        _map[x, y] = 1;
                    else if (neighborWallsCount < MapData.MinNeighborWalls)
                        _map[x, y] = 0;
                }
            }
        }

        public virtual int GetNeighborWallsCount(int x, int y)
        {
            int neighborCount = 0;
            for (int neighborX = x - 1; neighborX <= x + 1; neighborX++)
            {
                for (int neighborY = y - 1; neighborY <= y + 1; neighborY++)
                {
                    if (neighborX >= 0 && neighborY >= 0 && neighborX < MapData.Width && neighborY < MapData.Height)
                    {
                        if (neighborX != x || neighborY != y)
                            neighborCount += _map[neighborX, neighborY];
                    }
                    else
                        neighborCount++;
                }
            }
            return neighborCount;
        }

        public virtual Vector2 GetRandomEmptyPosition()
        {
            Vector2 position = new Vector2();
            do
            {
                position.x = Randomizer.RandomNumber(0, MapData.Width);
                position.y = Randomizer.RandomNumber(0, MapData.Height);
            }
            while (_map[(int)position.x, (int)position.y] == 1);

            position.x = -MapData.Width / 2 + position.x + 0.5F;
            position.y = -MapData.Height / 2 + position.y + 0.5F;
            return position;
        }


        // void OnDrawGizmos()
        // {
        //     if (_map != null)
        //     {
        //         for (int x = 0; x < MapData.Width; x++)
        //         {
        //             for (int y = 0; y < MapData.Height; y++)
        //             {
        //                 Gizmos.color = _map[x, y] == 1 ? Color.black : Color.white;
        //                 Vector3 pos = new Vector3(-MapData.Width / 2 + x + 0.5F, -MapData.Height / 2 + y + 0.5F);
        //                 Gizmos.DrawCube(pos, Vector3.one);
        //             }
        //         }
        //     }
        // }

        public int[,] Map { get => _map; }
    }
}

