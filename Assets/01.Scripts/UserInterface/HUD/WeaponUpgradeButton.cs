using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace Penwyn.Game
{
    public class WeaponUpgradeButton : MonoBehaviour
    {
        [Header("General")]
        public Button Button;
        public Image Icon;
        public TMP_Text Name;
        public TMP_Text Description;
        public BowData Data;

        [Header("Attributes")]
        public TMP_Text HPCostTxt;
        public TMP_Text AttackTxt;
        public TMP_Text NumOfBulletsTxt;
        public TMP_Text ASPDTxt;



        public event UnityAction DataChosen;

        public virtual void Set(BowData data)
        {
            BowData oldData = (BowData)PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData;
            this.Data = data;
            if (Icon != null)
                Icon.sprite = data.Icon;
            Name?.SetText(data.Name);
            Description?.SetText(data.Description);
            AttackTxt?.SetText($"<color={GetComparedValuesTextColor(oldData.Damage, data.Damage, "red", "green")}>{data.Damage}</color>");
            NumOfBulletsTxt?.SetText($"<color={GetComparedValuesTextColor(oldData.BulletPerShot, data.BulletPerShot, "red", "green")}>{data.BulletPerShot}</color>");
            ASPDTxt?.SetText($"<color={GetComparedValuesTextColor(oldData.Cooldown, data.Cooldown, "green", "red")}>{data.AttackSpeed}</color>");
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(UpgradeChosen);
        }

        public virtual void UpgradeChosen()
        {
            if (Data != null)
            {
                PlayerManager.Instance.Player.CharacterWeaponHandler.CurrentWeapon.Upgrade(Data);
                DataChosen?.Invoke();
            }
        }

        public string GetComparedValuesTextColor(float value1, float value2, string biggerColorRichText, string smallerColorRichText)
        {
            return value1 > value2 ? biggerColorRichText : smallerColorRichText;
        }
    }
}

