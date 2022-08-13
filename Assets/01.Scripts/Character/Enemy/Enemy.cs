using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Enemy : Character
    {
        public AIBrain AIBrain;

        protected bool newData;

        protected override void Awake()
        {
            base.Awake();
            if (AIBrain == null)
                AIBrain = GetComponent<AIBrain>();
        }
        
        public virtual void Load(EnemyData data)
        {
            base.Load(data);
            newData = false;
            if (this.Data != data)
                newData = true;
            LoadEnemy();
        }

        public virtual void LoadEnemy()
        {
            if (newData)
            {
                this._characterRun.RunSpeed = Data.MoveSpeed;
                this._characterWeaponHandler.ChangeWeapon(Data.WeaponData);
            }
        }

        public EnemyData Data { get => (EnemyData)_data; }
    }
}
