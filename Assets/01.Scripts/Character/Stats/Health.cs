using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class Health : MonoBehaviour
    {
        [Header("Display")]
        public WorldSpaceProgressBar HPValueBar;

        [Header("Damage Flicker")]
        public float DamageFlickerDuration = 1;

        [Header("Damaged Feedback")]
        public Color DamageTakenFlickerColor = Color.red;

        [Header("Feedbacks")]
        public Feedbacks HitFeedbacks;
        public Feedbacks DeathFeedbacks;

        [SerializeField][ReadOnly] private FloatValue _value;

        protected float _damageFlickerTime = 0;
        protected Character _character;



        public event UnityAction<Character> OnDeath;

        protected virtual void Awake()
        {
            _character = GetComponent<Character>();
            _value = new FloatValue();
            _value.CanBeHigherThanBase = false;
            _value.CanBeNegative = true;
            HPValueBar.AssignValue(_value);
        }

        #region Damage Taken

        public virtual void Take(float damage)
        {
            if (_value.CurrentValue > 0)
            {
                _value.CurrentValue -= damage;
                _value.CurrentValue = Mathf.Clamp(_value.CurrentValue, 0, _value.BaseValue);
                if (HitFeedbacks != null)
                    HitFeedbacks.PlayFeedbacks();
                if (_value.CurrentValue > 0)
                {
                    PlayDamageTakenVFX();
                }
                else
                {
                    if (DeathFeedbacks != null)
                        DeathFeedbacks.PlayFeedbacks();
                    Kill();
                }
            }
        }

        /// <summary>
        /// Lose flat HP. No invulnerable started.
        /// </summary>
        /// <param name="health">Amount</param>
        public virtual void Lose(float health)
        {
            _value.CurrentValue -= health;
            _value.CurrentValue = Mathf.Clamp(_value.CurrentValue, 0, _value.BaseValue);
            if (_value.CurrentValue <= 0)
                Kill();
        }

        /// <summary>
        /// Get a flat amount of HP, positive value only.
        /// </summary>
        /// <param name="health"></param>
        public virtual void Get(float health)
        {
            if (health < 0)
                return;
            _value.CurrentValue += health;
            _value.CurrentValue = Mathf.Clamp(_value.CurrentValue, 0, _value.BaseValue);
        }

        /// <summary>
        /// Get a flat amount of HP, positive value only.
        /// </summary>
        /// <param name="curretHealth">Current HP</param>
        /// <param name="maxHealth">Max HP</param>
        public virtual void Set(float curretHealth, float maxHealth)
        {
            _value.BaseValue = maxHealth;
            _value.CurrentValue = curretHealth;
        }

        #endregion

        #region Kill
        public virtual void Kill()
        {
            _value.CurrentValue = 0;
            gameObject.SetActive(false);
        }
        #endregion

        #region Damage Taken VFX

        public void PlayDamageTakenVFX()
        {
            if (DamageFlickerDuration > 0)
                StartCoroutine(TakeDamageVFXCoroutine());
        }

        protected virtual IEnumerator TakeDamageVFXCoroutine()
        {
            _damageFlickerTime = 0;
            Coroutine flicker = StartCoroutine(SpriteRendererUtil.Flicker(_character.SpriteRenderer, DamageTakenFlickerColor, DamageFlickerDuration, 0.1F));
            while (_damageFlickerTime < DamageFlickerDuration)
            {
                _damageFlickerTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion

        public virtual void Reset()
        {
            StopAllCoroutines();
            if (_character != null && _character.SpriteRenderer != null)
                _character.SpriteRenderer.color = Color.white;
        }

        public virtual void OnEnable()
        {
            _damageFlickerTime = 0;
        }

        public virtual void OnDisable()
        {
            Reset();
        }

        public FloatValue Value { get => _value; }
        public float MaxHealth { get => _value.BaseValue; }
        public float CurrentHealth { get => _value.CurrentValue; }

    }
}
