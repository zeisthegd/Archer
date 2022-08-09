using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Tile Settings")]
        public Tilemap GroundTilemap;
        public Tilemap WallTilemap;
        public PolygonCollider2D LevelBounds;
        protected LevelGenerator _generator;

        public virtual void BuildMap(LevelGenerator generator)
        {
            _generator = generator;
            SetWallTiles();
            SetLevelBounds();
        }

        public virtual void SetWallTiles()
        {
            int[,] walls = _generator.Map;
            Vector3Int tilePos;
            for (int x = 0; x < _generator.MapData.Width; x++)
            {
                for (int y = 0; y < _generator.MapData.Height; y++)
                {
                    tilePos = new Vector3Int(-_generator.MapData.Width / 2 + x, -_generator.MapData.Height / 2 + y);
                    if (walls[x, y] == 1)
                    {
                        WallTilemap.SetTile(tilePos, _generator.MapData.WallTile);
                    }
                    GroundTilemap.SetTile(tilePos, _generator.MapData.GroundTile);
                }
            }
        }

        public virtual void SetLevelBounds()
        {
            float width = _generator.MapData.Width / 2;
            float height = _generator.MapData.Height / 2;
            LevelBounds.SetPath(0, new Vector2[] { new Vector2(width, height), new Vector2(-width, height), new Vector2(-width, -height), new Vector2(width, -height) });
        }
    }
}

