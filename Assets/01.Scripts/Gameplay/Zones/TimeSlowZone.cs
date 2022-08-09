using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

using DG.Tweening;


namespace Penwyn.Game
{
    public class TimeSlowZone : MonoBehaviour
    {
        [ReadOnly] public float Radius = 360;
        [ReadOnly] public float SlowTimeScale = 0.5F;
        public float NormalizeDuration = 1;
        public ZoneState State;

        protected float _range;
        protected float _closeDuration;
        protected float _recoverDuration;
        protected float _delayBeforeRecoverDuration;

        protected List<GameObject> _objectsInRange = new List<GameObject>();
        protected CharacterTimeZoneControl _characterTZControl;

        public virtual void Initialization(CharacterTimeZoneControl characterTimeZoneControl)
        {
            _characterTZControl = characterTimeZoneControl;
            _range = _characterTZControl.TimeZoneRange;
            _closeDuration = _characterTZControl.ZoneClosingDuration;
            _recoverDuration = _characterTZControl.RecoverDuration;
            _delayBeforeRecoverDuration = _characterTZControl.DelayBeforeRecoverDuration;

            SlowTimeScale = _characterTZControl.SlowTimeScale;
            transform.localScale = Vector3.one * _range;
        }

        /// <summary>
        /// Reduce the scale to 0, disable the grab.
        /// </summary>
        public virtual void CloseZone()
        {
            State = ZoneState.Closing;
            transform.DOKill();
            transform.DOScale(Vector3.zero, _closeDuration).onComplete += () =>
            {
                StartCoroutine(RecoverZoneScale());
            };
        }

        /// <summary>
        /// Scale back to normal scale.
        /// </summary>
        public virtual IEnumerator RecoverZoneScale()
        {
            transform.DOKill();
            yield return new WaitForSeconds(_delayBeforeRecoverDuration);
            State = ZoneState.Recovering;
            transform.DOScale(Vector3.one * _range, _recoverDuration).onComplete += () =>
            {
                State = ZoneState.Idle;
            };
        }

        /// <summary>
        /// Slow entering object if it has a Slowable Behavior.
        /// </summary>
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            Slowable slowableObject = other.gameObject.GetComponent<Slowable>();
            if (Radius > 0 && slowableObject && !_objectsInRange.Contains(other.gameObject))
            {
                _objectsInRange.Add(other.gameObject);
                slowableObject?.Slow(SlowTimeScale);
            }
        }

        /// <summary>
        /// If the exitted object has a Slowable Behavior, normalize its time scale.
        /// </summary>
        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (_objectsInRange.Contains(other.gameObject))
            {
                _objectsInRange.Remove(other.gameObject);
                if (other.gameObject.activeInHierarchy)
                    other.gameObject.GetComponent<Slowable>().Normalize(NormalizeDuration);
            }
        }

        public bool IsUseable => State != ZoneState.Grabbing && State != ZoneState.Closing && transform.localScale.magnitude > 0;

        public List<GameObject> ObjectsInRange { get => _objectsInRange; }
    }

    public enum ZoneState
    {
        Idle,
        Grabbing,
        Closing,
        Recovering
    }
}
