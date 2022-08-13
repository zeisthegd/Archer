using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Player : Character
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public virtual void Load(PlayerData data)
        {
            base.Load(data);
            Debug.Log(this._characterWeaponHandler);
            this._characterWeaponHandler.ChangeWeapon(Data.StartingBow);
        }

        public PlayerData Data { get => (PlayerData)_data; }
    }
}
