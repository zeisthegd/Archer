using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
namespace Penwyn.Tools
{
    public class AddMoneyAction : MonoBehaviour
    {
        public int Amount = 1;

        public virtual void AddMoneyTo()
        {
            PlayerManager.Instance.Player.CharacterMoney.Add(Amount);
        }
    }
}

