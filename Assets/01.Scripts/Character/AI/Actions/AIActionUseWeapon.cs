using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class AIActionUseWeapon : AIAction
    {
        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
            EnsureAbilityComponents();
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
            RotateWeaponToPlayer();
            _character.CharacterWeaponHandler.CurrentWeapon.RequestWeaponUse();
        }

        public override void StateEnter()
        {
            base.StateEnter();
        }

        public override void StateExit()
        {
            base.StateExit();
        }

        protected virtual void RotateWeaponToPlayer()
        {
            Vector3 dirToPlayer = (PlayerManager.Instance.Player.transform.position - _character.CharacterWeaponHandler.CurrentWeapon.transform.position).normalized;
            _character.CharacterWeaponHandler.CurrentWeapon.transform.right = dirToPlayer;
        }

        public virtual void EnsureAbilityComponents()
        {
            if (_character.CharacterWeaponHandler == null || _character.CharacterWeaponHandler.CurrentWeapon == null)
            {
                this.enabled = false;
                Debug.LogWarning("No CharacterWeaponHandler or Weapon equipped!");
            }
        }
    }

}
