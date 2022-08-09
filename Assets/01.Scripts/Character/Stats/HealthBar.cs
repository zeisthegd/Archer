using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Bar Spawning")]
        public Canvas Canvas;
        public ProgressBar HealthBarPrefab;
        public Vector3 HealthBarOffset;

        protected ProgressBar _currentHPBar;
        protected Health _health;
        protected Canvas _canvas;

        protected virtual void Awake()
        {
            _health = GetComponent<Health>();
        }
        protected virtual void Update()
        {
            _currentHPBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + HealthBarOffset);
        }
        public virtual void Initialization()
        {
            CreateHealthSliders();
            SetHealthSlidersValue();
            SetSlidersLength();
        }

        /// <summary>
        /// Instantiate the bars from prefabs.
        /// </summary>
        public virtual void CreateHealthSliders()
        {
            if (_currentHPBar == null)
            {
                _canvas = Instantiate(Canvas, transform.position, Quaternion.identity);
                _canvas.transform.SetParent(this.transform);
                _currentHPBar = Instantiate(HealthBarPrefab, transform.position, Quaternion.identity, _canvas.transform);
                _currentHPBar.transform.SetParent(_canvas.transform);
            }
        }

        /// <summary>
        /// Set the 2 sliders' max value.
        /// </summary>
        public virtual void SetHealthSlidersValue()
        {
            if (_currentHPBar != null)
            {
                _currentHPBar.SetMaxValue(_health.StartingHealth);
                _currentHPBar.SetValue(_health.CurrentHealth);
            }
        }

        /// <summary>
        /// Set the sliders' length based on the sprite size.
        /// </summary>
        public virtual void SetSlidersLength()
        {
            if (_currentHPBar)
            {
                Vector2 sizeOfSlider = new Vector2(_health.Character.SpriteRenderer.sprite.rect.width * 2, _currentHPBar.ActualValue.GetComponent<RectTransform>().sizeDelta.y);
                _currentHPBar.ActualValue.GetComponent<RectTransform>().sizeDelta = sizeOfSlider;
                _currentHPBar.LostValue.GetComponent<RectTransform>().sizeDelta = sizeOfSlider;
            }
        }
        public virtual void SetHealth()
        {
            if (_currentHPBar)
            {
                _currentHPBar.SetValue(_health.CurrentHealth);
            }
        }
        public virtual void SetHealth(float newHealth)
        {
            if (_currentHPBar)
            {
                _currentHPBar.SetValue(newHealth);
            }
        }

        public virtual void OnEnable()
        {
            _health.OnChanged += SetHealth;
        }

        public virtual void OnDisable()
        {
            _health.OnChanged -= SetHealth;

        }
    }
}

