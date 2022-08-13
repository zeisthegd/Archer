using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Game/Character/Weapon/Data", fileName = "NewWeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Header("Name")]
        public string Name = "No Name";

        [Header("Description")]
        public string Description = "Description Here.";

        [Header("Graphics")]
        public Sprite Icon;
        public AnimatorOverrideController AnimatorOverride;

        [Header("Bullet")]
        public Projectile Projectile;
        public float Damage = 1;

        [Header("Iterations")]
        [Tooltip("Number of times to shoot in a single input")]
        public int Iteration = 1;
        public float DelayBetweenIterations = 0.1F;

        [Header("Attack Settings")]
        public int BulletPerShot = 1;
        public float DelayBetweenBullets = 0.1F;
        public float Angle = 0;
        public float Cooldown = 1;
        public bool DisableAimingWhenShooting = false;

        [Header("Feedbacks")]
        public Feedbacks WeaponUseFeedbacks;

        [Header("Upgrade")]
        public List<WeaponData> Upgrades;
        public int RequiredUpgradeValue = 0;
    }

}

