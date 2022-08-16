using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class Weapon : MonoBehaviour
    {
        [Header("Data")]
        [Expandable]
        public WeaponData CurrentData;
        [HorizontalLine]

        [Header("Graphics")]
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        [HorizontalLine]

        [Header("Input")]
        public WeaponInputType InputType;

        [Header("Owner")]
        [ReadOnly] public Character Owner;

        [ReadOnly][SerializeField] protected StateMachine<WeaponState> _weaponState = new StateMachine<WeaponState>(WeaponState.WeaponIdle);// Weapon usage state.
        protected WeaponAim _weaponAim;
        protected WeaponAutoAim _weaponAutoAim;
        protected Coroutine _cooldownCoroutine;
        protected Feedbacks _weaponUseFeedbacks;


        public event UnityAction RequestUpgradeEvent;

        protected virtual void Awake()
        {

        }

        public virtual void Initialize()
        {
            GetComponents();
        }
        protected virtual void Start()
        {
        }
        protected virtual void Update()
        {
        }


        public virtual void RequestWeaponUse()
        {
            //*Derive this
            if (_weaponState.Is(WeaponState.WeaponIdle))
            {
                UseWeapon();
            }
        }

        protected virtual void UseWeapon()
        {
            _weaponState.Change(WeaponState.WeaponUse);
        }

        public virtual void UseWeaponTillNoTarget()
        {
            if (_weaponAutoAim)
            {
                _weaponAutoAim.FindTarget();
            }
            RequestWeaponUse();
        }

        public virtual void StartCooldown()
        {
            _weaponState.Change(WeaponState.WeaponCooldown);
            _cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        }

        protected virtual IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(CurrentData.Cooldown);
            _weaponState.Change(WeaponState.WeaponIdle);
        }

        /// <summary>
        /// Load the weapon data from a scriptable data.
        /// </summary>
        public virtual void LoadWeapon(WeaponData data)
        {
            CurrentData = data;
            gameObject.name = data.Name;
            _weaponUseFeedbacks = Instantiate(CurrentData.WeaponUseFeedbacks, this.transform.position, Quaternion.identity, this.transform);
        }

        [Button("Load Weapon Data")]
        public virtual void LoadWeapon()
        {
            if (CurrentData != null)
                LoadWeapon(CurrentData);
            else
                Debug.Log("Please insert Weapon Data");
        }


        #region Upgrade

        [Button("Reques Upgrade", EButtonEnableMode.Playmode)]
        public virtual void RequestUpgrade()
        {
            RequestUpgradeEvent?.Invoke();
        }

        [Button("Upgrade (Random Data)")]
        public virtual void RandomUpgrade()
        {
            Upgrade(CurrentData.Upgrades[Randomizer.RandomNumber(0, CurrentData.Upgrades.Count)]);
        }

        public virtual void Upgrade(WeaponData data)
        {
            if (CurrentData != null)
            {
                if (CurrentData.Upgrades != null)
                {
                    LoadWeapon(data);
                }
                else
                {
                    Debug.Log("Max level reached!");
                }
            }
            else
                Debug.Log("Please insert Weapon Data!");
        }

        #endregion

        public virtual void GetComponents()
        {
            _weaponAim = GetComponent<WeaponAim>();
            _weaponAutoAim = GetComponent<WeaponAutoAim>();
        }

        protected virtual void OnEnable()
        {
            _weaponState.Change(WeaponState.WeaponIdle);
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
