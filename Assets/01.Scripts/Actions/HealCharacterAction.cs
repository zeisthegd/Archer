using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
namespace Penwyn.Tools
{
    public class HealCharacterAction : MonoBehaviour
    {
        public int Amount = 1;

        public virtual void HealPlayer()
        {
            Heal(PlayerManager.Instance.Player);
        }
        
        public virtual void Heal(Character character)
        {
            character.Health.Get(Amount);
        }
    }
}

