using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;

using Penwyn.Game;
using Penwyn.Tools;
using System;

namespace Penwyn.UI
{
    public class HUD : SingletonMonoBehaviour<HUD>
    {
        [Header("Player")]
        public ProgressBar PlayerHealth;
        public ProgressBar PlayerEXP;
        public Button WeaponButton;
        public List<WeaponUpgradeButton> WeaponUpgradeButtons;

        [Header("Level")]
        public ProgressBar LevelProgress;


        protected virtual void Awake()
        {
            PlayerManager.Instance.PlayerSpawned += OnPlayerSpawned;
            LevelManager.Instance.Loaded += OnLevelLoaded;
        }


        #region Player HUD #####################################################################
        private void OnPlayerSpawned()
        {
            SetUpPlayerInfo();
            SetWeaponButtonIcon();
            ConnectEvents();
        }

        public virtual void SetUpPlayerInfo()
        {
            PlayerHealth?.AssignStoredValue(PlayerManager.Instance.Player.Health.Value);
            PlayerEXP?.AssignStoredValue(PlayerManager.Instance.Player.Experience);
        }

        #endregion

        #region Level HUD #####################################################################

        private void OnLevelLoaded()
        {
            SetUpLevelProgressInfo();
        }

        private void OnBossSpawned()
        {
            SetUpLevelBossInfo();
        }


        public virtual void SetUpLevelProgressInfo()
        {
            LevelProgress?.AssignStoredValue(LevelManager.Instance.Progress);
        }

        public virtual void SetUpLevelBossInfo()
        {
            LevelProgress?.RemoveValue();
            LevelProgress?.AssignStoredValue(LevelManager.Instance.EnemySpawner.Boss.Health.Value);
        }

        #endregion

        #region Weapon Upgrades #####################################################################

        public virtual void LoadAvailableUpgrades()
        {
            if (PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count <= 0)
                return;
            Time.timeScale = 0;
            for (int i = 0; i < PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count; i++)
            {
                WeaponUpgradeButtons[i].Set((BowData)PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades[i]);
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
                PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent += LoadAvailableUpgrades;
            }
            ConnectEndWeaponUpgradesEvents();
        }

        public virtual void DisconnectEvents()
        {
            if (PlayerManager.Instance != null && PlayerManager.Instance.Player != null)
            {
                PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent -= LoadAvailableUpgrades;
            }
            DisconnectEndWeaponUpgradesEvents();
        }
    }
}

