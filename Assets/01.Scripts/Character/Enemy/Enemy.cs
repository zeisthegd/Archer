using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Enemy : Character
    {
        public AIBrain AIBrain;
        public EnemyData InitData;

        protected EnemyData _data;

        protected bool newData;

        protected override void Awake()
        {
            base.Awake();
            if (AIBrain == null)
                AIBrain = GetComponent<AIBrain>();
        }

        public virtual void LoadEnemy(EnemyData data)
        {
            newData = false;
            if (this._data != data)
                newData = true;
            this._data = data;
            LoadEnemy();
        }

        public virtual void LoadEnemy()
        {
            this.Health.Set(_data.Health, _data.Health);
            if (newData)
            {
                this.Animator.runtimeAnimatorController = _data.RuntimeAnimatorController;
                this._characterRun.RunSpeed = _data.MoveSpeed;
                this._characterWeaponHandler.ChangeWeapon(_data.WeaponData);
            }
        }
        
        public EnemyData Data { get => _data; }
    }
}
