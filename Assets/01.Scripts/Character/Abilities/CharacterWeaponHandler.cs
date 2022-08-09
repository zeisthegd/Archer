using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class CharacterWeaponHandler : CharacterAbility
    {
        [Header("Weapon Data")]
        public Weapon InitialWeaponPrefab;
        public WeaponData InitialWeaponData;
        [HorizontalLine]

        [Header("Weapon Holder")]
        public Transform WeaponHolder;

        protected Weapon _currentWeapon;
        protected WeaponData _currentWeaponData;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            CreateWeapon();
        }
        public virtual void CreateWeapon()
        {
            if (WeaponHolder == null)
                WeaponHolder = this.transform;

            _currentWeapon = Instantiate(InitialWeaponPrefab, WeaponHolder.position, Quaternion.identity, WeaponHolder);
            _currentWeapon.Owner = this._character;
            _currentWeapon.Initialization();
            _currentWeapon.LoadWeapon(InitialWeaponData);
        }

        public virtual void ChangeWeapon(WeaponData newData)
        {
            if (_currentWeapon == null)
                CreateWeapon();
            else
            {
                _currentWeapon.LoadWeapon(newData);
            }
        }

        public Weapon CurrentWeapon { get => _currentWeapon; }
        public WeaponData WeaponData { get => _currentWeaponData; }
    }
}