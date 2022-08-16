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
            LoadEnemy();
        }

        public virtual void LoadEnemy()
        {
            this.CharacterRun.RunSpeed = Data.MoveSpeed;
            this.CharacterWeaponHandler.ChangeWeapon(Data.WeaponData);
            this.AIBrain = Instantiate(Data.Brain, transform.position, Quaternion.identity, transform);
            this.AIBrain.InitializeWith(this);
            this.AIBrain.Enabled = true;
        }

        public EnemyData Data { get => (EnemyData)BaseData; }
    }
}
