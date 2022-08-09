using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using NaughtyAttributes;

using Penwyn.Tools;


namespace Penwyn.Game
{
    public class Slowable : MonoBehaviour
    {
        [InfoBox("This components will slow animations speed, rigidbody2D's velocity when the gameObject enter a SlowTimeZone.", EInfoBoxType.Normal)]
        protected bool _isSlowed;
        protected float _currentScale;
        protected Vector2 _velocityJustAfterSlowed;
        protected CharacterController _controller;
        protected Animator _animator;

        protected Coroutine _normalizeCoroutine;


        protected virtual void Awake()
        {
            _controller = gameObject.FindComponent<CharacterController>();
            _animator = gameObject.FindComponent<Animator>();

            CharacterTimeZoneControl playerTimeZoneControlAbility = PlayerManager.Instance.Player?.FindAbility<CharacterTimeZoneControl>();
            if (playerTimeZoneControlAbility)
                playerTimeZoneControlAbility.TimeScaleChanged += OnTimeScaleChanged;

        }

        protected virtual void Update()
        {
            if (IsSlowed)
                _controller?.SetVelocity(_velocityJustAfterSlowed * _currentScale);
        }

        protected virtual void UpdateSlowState()
        {

        }

        protected virtual void UpdateNormalState()
        {

        }

        /// <summary>
        /// Slow this gameObject.
        /// </summary>
        public virtual void Slow(float scale)
        {
            if (_normalizeCoroutine != null)
                StopCoroutine(_normalizeCoroutine);
            if (!_isSlowed && scale < 1)
            {
                _isSlowed = true;
                ChangeTimeScale(scale);
                _velocityJustAfterSlowed = _controller.Velocity;
            }
        }

        public virtual void Normalize(float duration)
        {
            _normalizeCoroutine = StartCoroutine(NormalizeCoroutine(duration));
        }

        /// <summary>
        /// Slowly move recover the timescale.
        /// </summary>
        public IEnumerator NormalizeCoroutine(float duration)
        {
            float time = 0;

            while (time < duration && this.gameObject.activeInHierarchy)
            {
                _currentScale = time / duration;
                SetAnimatorSpeed(time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            _isSlowed = false;
            _currentScale = 1;
            _controller.SetVelocity(_velocityJustAfterSlowed);
            SetAnimatorSpeed(1);
        }

        protected virtual void OnTimeScaleChanged(float scale)
        {
            ChangeTimeScale(scale);
        }

        protected virtual void ChangeTimeScale(float scale)
        {
            if (IsSlowed)
            {
                _currentScale = scale;
                SetAnimatorSpeed(scale);
            }
        }

        protected virtual void SetAnimatorSpeed(float speed)
        {
            if (_animator)
                _animator.speed = speed;
        }

        protected virtual void OnEnable()
        {
            _currentScale = 1;
            _isSlowed = false;
            SetAnimatorSpeed(1);
        }
        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        public bool IsSlowed { get => _isSlowed; }
        public float CurrentScale { get => _currentScale; }
    }
}
