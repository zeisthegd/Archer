using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class CharacterDash : CharacterAbility
    {
        public float DashDuration = 0.5F;
        public float DashSpeed = 1F;

        protected bool _isDashing = false;
        protected Vector2 _startDashInput = Vector2.zero;
        protected float _dashTime = 0;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
        }
        public override void FixedUpdateAbility()
        {
            Dash();
        }
        public virtual void StartDash()
        {
            if (_isDashing == false)
            {
                _isDashing = true;
                _dashTime = DashDuration;
                _startDashInput = InputReader.Instance.MoveInput;
            }
        }

        public virtual void StopDash()
        {
            if (_isDashing == true)
            {
                _isDashing = false;
            }
        }

        protected virtual void Dash()
        {
            if (_isDashing)
            {
                _controller.SetVelocity(_startDashInput.normalized * DashSpeed);
                _dashTime -= Time.deltaTime;
                if (_dashTime <= 0)
                {
                    StopDash();
                }
            }
        }

        public override void ConnectEvents()
        {
            base.ConnectEvents();
            InputReader.Instance.DashPressed += StartDash;
        }

        public override void DisconnectEvents()
        {
            base.ConnectEvents();
            InputReader.Instance.DashPressed -= StartDash;
        }
    }
}
