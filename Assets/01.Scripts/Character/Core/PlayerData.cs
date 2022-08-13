using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Character/Data/Player")]
    public class PlayerData : CharacterData
    {
        [Header("Weapon")]
        public BowData StartingBow;
    }
}