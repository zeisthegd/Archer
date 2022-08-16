using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class Player : Character
    {
        private FloatValue _experience;
        protected override void Awake()
        {
            base.Awake();
        }

        public virtual void Load(PlayerData data)
        {
            base.Load(data);
            _experience = new FloatValue(10);
            _experience.ReachedMax += OnMaxExperienceReached;
            this.CharacterWeaponHandler.ChangeWeapon(Data.StartingBow);
        }

        public void OnMaxExperienceReached()
        {
            CharacterWeaponHandler.CurrentWeapon.RequestUpgrade();
        }

        public PlayerData Data { get => (PlayerData)BaseData; }
        public FloatValue Experience { get => _experience; }
    }
}
