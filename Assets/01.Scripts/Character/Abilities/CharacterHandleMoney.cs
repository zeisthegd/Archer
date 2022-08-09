using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Penwyn.Game
{
    public class CharacterHandleMoney : CharacterAbility
    {
        public int CurrentMoney = 0;

        public event UnityAction MoneyChanged;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            Set(CurrentMoney);
        }

        public virtual void Add(int amount)
        {
            Set(CurrentMoney + amount);
        }

        public virtual void Set(int newMoney)
        {
            CurrentMoney = newMoney;
            MoneyChanged?.Invoke();
        }
    }
}
