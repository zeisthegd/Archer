using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

using Penwyn.Game;

namespace Penwyn.Tools
{
    [CreateAssetMenu(menuName = "Util/Input Reader")]
    public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
    {
        #region Gameplay Input Events

        //Movement
        public event UnityAction<Vector2> Move;

        //Action

        public event UnityAction DashPressed;
        public event UnityAction DashReleased;

        public event UnityAction AttackPressed;
        public event UnityAction AttackReleased;

        #endregion

        #region Logic Variables

        public bool IsHoldingAttack { get; set; }
        public bool IsHoldingDash { get; set; }

        #endregion

        private PlayerInput playerinput;

        private void OnEnable()
        {
            if (playerinput == null)
            {
                playerinput = new PlayerInput();
                playerinput.Gameplay.SetCallbacks(this);
            }
        }

        public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MoveInput = context.ReadValue<Vector2>();
                Move?.Invoke(MoveInput);
            }
            else
                MoveInput = Vector2.zero;
        }

        public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackPressed?.Invoke();
                IsHoldingAttack = true;
            }
            else if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
            {
                AttackReleased?.Invoke();
                IsHoldingAttack = false;
            }
        }

        public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Dash Pressed");
                DashPressed?.Invoke();
                IsHoldingDash = true;
            }
            else if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
            {
                DashReleased?.Invoke();
                IsHoldingDash = false;
            }
        }

        public void EnableGameplayInput()
        {
            playerinput.Gameplay.Enable();
        }

        public void DisableGameplayInput()
        {
            playerinput.Gameplay.Disable();
        }

        void OnDisable()
        {
            DisableGameplayInput();
        }

        public Vector2 MoveInput { get; set; }
    }
}
