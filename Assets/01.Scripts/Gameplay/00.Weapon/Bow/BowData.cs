using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Game/Character/Weapon/Bow Data", fileName = "NewBowData")]
    public class BowData : WeaponData
    {
        [Header("Attack Speed")]
        public float AttackSpeed = 2;
    }
}

