using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Character/Data/Enemy")]
    public class EnemyData : CharacterData
    {
        [Header("General")]
        public float MoveSpeed = 1;
        public float ThreatLevel = 1;
        public EnemyType Type;

        [Header("Weapon")]
        public WeaponData WeaponData;

        [Header("AI")]
        public AIBrain Brain;
    }
}