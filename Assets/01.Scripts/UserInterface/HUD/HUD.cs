using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;

using Penwyn.Game;
using Penwyn.Tools;

namespace Penwyn.UI
{
    public class HUD : SingletonMonoBehaviour<HUD>
    {
        [Header("Player")]
        public ProgressBar PlayerHealth;
        public TMP_Text PlayerMoney;
        public Button WeaponButton;
        public List<WeaponUpgradeButton> WeaponUpgradeButtons;

        protected virtual void Awake()
        {
            PlayerManager.PlayerSpawned += OnPlayerSpawned;
        }

        #region PlayerHUD

        public virtual void SetHealthBar()
        {
            if (PlayerHealth != null)
            {
                PlayerHealth.AssignStoredValue(PlayerManager.Instance.Player.Health.Value);
            }
        }

        protected virtual void UpdateMoney()
        {
            if (PlayerMoney != null)
                PlayerMoney.SetText(PlayerManager.Instance.Player.CharacterMoney.CurrentMoney + "");
        }

        #region Weapon Upgrades

        public virtual void LoadAvailableUpgrades()
        {
            if (PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count <= 0)
                return;
            Time.timeScale = 0;
            for (int i = 0; i < PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count; i++)
            {
                WeaponUpgradeButtons[i].Set(PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades[i]);
                WeaponUpgradeButtons[i].gameObject.SetActive(true);
            }
        }

        public virtual void EndWeaponUpgrades()
        {
            SetWeaponButtonIcon();
            Time.timeScale = 1;
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].gameObject.SetActive(false);
            }
        }

        public virtual void ConnectEndWeaponUpgradesEvents()
        {
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].DataChosen += EndWeaponUpgrades;
            }
        }

        public virtual void DisconnectEndWeaponUpgradesEvents()
        {
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].DataChosen -= EndWeaponUpgrades;
            }
        }

        public virtual void SetWeaponButtonIcon()
        {
            if (WeaponButton != null)
                WeaponButton.image.sprite = PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Icon;
        }

        #endregion

        protected virtual void OnPlayerSpawned()
        {
            SetHealthBar();
            SetWeaponButtonIcon();
            ConnectEvents();
        }

        #endregion

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {
            DisconnectEvents();
        }

        public virtual void ConnectEvents()
        {
            if (PlayerManager.Instance != null && PlayerManager.Instance.Player != null)
            {
                PlayerManager.Instance.Player.CharacterMoney.MoneyChanged += UpdateMoney;
                PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent += LoadAvailableUpgrades;
            }
            ConnectEndWeaponUpgradesEvents();
        }

        public virtual void DisconnectEvents()
        {
            if (PlayerManager.Instance != null && PlayerManager.Instance.Player != null)
            {
                PlayerManager.Instance.Player.CharacterMoney.MoneyChanged -= UpdateMoney;
                PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent -= LoadAvailableUpgrades;
            }
            DisconnectEndWeaponUpgradesEvents();
        }
    }
}

