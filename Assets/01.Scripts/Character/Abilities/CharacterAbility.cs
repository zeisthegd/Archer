using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class CharacterAbility : MonoBehaviour
    {
        public bool AbilityPermitted = true;
        public string Name;

        public List<CharacterAbilityStates> ForbidPermissionCharacterStates;
        public List<ControllerState> ForbidPermissionControllerStates;

        protected Character _character;
        protected CharacterController _controller;

        bool abilityAuthorized;

        public virtual void AwakeAbility(Character character)
        {
            this._character = character;
            this._controller = character.Controller;
            if (AbilityPermitted)
            {
                ConnectEvents();
            }
        }

        public void ChangePermission(bool permission)
        {
            if (permission)
            {
                ConnectEvents();
            }
            else
            {
                DisconnectEvents();
            }
        }

        public virtual void UpdateAbility() { }
        public virtual void FixedUpdateAbility() { }
        public virtual void ConnectEvents() { }
        public virtual void DisconnectEvents() { }
        public virtual void OnDisable() { }
        public bool AbilityAuthorized
        {
            get
            {
                foreach (CharacterAbilityStates state in ForbidPermissionCharacterStates)
                {
                    if (_character.States.CurrentState == state)
                        return false;
                }

                foreach (ControllerState state in ForbidPermissionControllerStates)
                {
                    if (_controller.States.CurrentState == state)
                        return false;
                }

                return AbilityPermitted;
            }
        }

    }
}