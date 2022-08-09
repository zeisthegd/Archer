using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class LootDropManager : SingletonMonoBehaviour<LootDropManager>
    {
        public MapData MapData;
        public DestructileTilemap _destructileTilemap;
        protected ObjectPooler _coinPooler;

        protected virtual void Awake()
        {
            _coinPooler = GetComponent<ObjectPooler>();
        }

        public virtual void HandleEnemyDeath(Character enemy)
        {
            SpawnEnemyCoinDrop(GetMoneyDropSettings(enemy.GetComponent<Enemy>().Data.Type), enemy.Position);
        }

        public virtual void SpawnEnemyCoinDrop(CoinDrop coinDrop, Vector3 spawnPos)
        {
            GameObject moneyObj = _coinPooler.PullOneObject();
            moneyObj.GetComponent<SpriteRenderer>().sprite = coinDrop.CoinSprite;
            moneyObj.GetComponent<HealCharacterAction>().Amount = coinDrop.HealAmount;
            moneyObj.GetComponent<AddMoneyAction>().Amount = coinDrop.MoneyAmount;
            moneyObj.transform.position = spawnPos;
            moneyObj.SetActive(true);
        }

        public virtual CoinDrop GetMoneyDropSettings(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Lesser:
                    return MapData.LesserMoneyDrop;
                case EnemyType.Greater:
                    return MapData.GreaterMoneyDrop;
                case EnemyType.Elite:
                    return MapData.EliteMoneyDrop;
                default:
                    return new CoinDrop();
            }
        }

        public virtual void HandleBossEnemyDeath()
        {

        }

        public virtual void HandleWallDestroyed(Vector3 wallPosition)
        {
            Vector3 truePos = new Vector3(wallPosition.x + 0.5F, wallPosition.y + 0.5F, 0);
            
        }

        protected virtual void OnEnable()
        {
            _destructileTilemap.TileDestroyed += HandleWallDestroyed;
        }
        protected virtual void OnDisable()
        {
            _destructileTilemap.TileDestroyed -= HandleWallDestroyed;
        }

    }
}

