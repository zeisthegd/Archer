using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class Bow : ProjectileWeapon
    {
        public Transform ArrowPosition;
        public WorldSpaceProgressBar ChargeTimeBar;

        private StateMachine<BowChargeState> _bowChargeState = new StateMachine<BowChargeState>(BowChargeState.Idle);
        private FloatValue _chargeTime = new FloatValue(0, 100);
        private int _chargedArrowCount = 0;

        protected override void Awake()
        {
            base.Awake();
            ChargeTimeBar.AssignValue(_chargeTime);
            transform.SetParent(null);
            _chargedArrowCount = 0;
        }

        protected override void Start()
        {
            base.Start();
            ConnectEvents();
        }

        protected override void Update()
        {
            base.Update();
            transform.position = Owner.Position;
            HandleBowChargeState();
        }

        protected virtual void HandleBowChargeState()
        {
            switch (_bowChargeState.CurrentState)
            {
                case BowChargeState.Idle:
                    HandleIdleState();
                    break;
                case BowChargeState.Charging:
                    HandleChargingState();
                    break;
                default:
                    break;
            }
        }

        public void OnAttackPressed()
        {
            if (_bowChargeState.Is(BowChargeState.Idle) && _weaponState.Is(WeaponState.WeaponIdle))
            {
                _bowChargeState.Change(BowChargeState.Charging);
            }
        }

        public void OnAttackReleased()
        {
            if (_bowChargeState.Is(BowChargeState.Charging))
            {
                Shoot();
                _bowChargeState.Change(BowChargeState.Idle);
            }
        }

        public void HandleIdleState()
        {
            _chargeTime.CurrentValue -= Data.AttackSpeed * Time.deltaTime;
        }

        public void HandleChargingState()
        {
            _chargeTime.CurrentValue += Data.AttackSpeed * Time.deltaTime;
            if (_chargeTime.CurrentValue >= _chargeTime.BaseValue)
            {
                _chargeTime.CurrentValue = 0;
                _chargedArrowCount += 1;
            }
        }

        private void Shoot()
        {
            Data.Iteration = _chargedArrowCount;
            _chargedArrowCount = 0;
            RequestWeaponUse();
        }

        private void ConnectEvents()
        {
            GameManager.Instance.InputReader.AttackPressed += OnAttackPressed;
            GameManager.Instance.InputReader.AttackReleased += OnAttackReleased;
        }

        private void DisonnectEvents()
        {
            GameManager.Instance.InputReader.AttackPressed -= OnAttackPressed;
            GameManager.Instance.InputReader.AttackReleased -= OnAttackReleased;
        }

        public BowData Data => (BowData)CurrentData;
    }

    public enum BowChargeState
    {
        Idle,
        Charging,
        Shooting
    }
}
