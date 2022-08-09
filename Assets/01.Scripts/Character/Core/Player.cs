using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Player : Character
    {
        public EnemyData InitData;

        protected EnemyData _data;

        protected bool newData;

        protected override void Awake()
        {
            base.Awake();
        }

        public virtual void Load(EnemyData data)
        {

        }
        
        public EnemyData Data { get => _data; }
    }
}
