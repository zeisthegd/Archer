using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Game/Character/Weapon/Data", fileName = "NewWeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Header("Name")]
        public string Name = "No Name";
        [Header("Description")]
        public string Description = "Description Here";

        [Header("Graphics")]
        public Sprite Icon;
        public Animator Animator;

        [Header("Bullet")]
        public Projectile Projectile;
        public float Damage = 1;

        [Header("Iterations")]
        [Tooltip("Number of times to shoot in a single input")]
        public int Iteration = 1;
        public float DelayBetweenIterations = 0.1F;
        [Header("Settings")]
        public int BulletPerShot = 1;
        public float DelayBetweenBullets = 0.1F;
        public float Angle = 0;
        public float Cooldown = 1;
        public bool DisableAutoAimWhenShooting = false;
        [Header("Energy Requirements")]
        public bool RequiresHealth = false;
        public float HealthPerUse = 0;
        [Header("Add-On")]
        public bool ProjectileRotating = false;
        public float RotateSpeed = 1;

        [Header("Upgrade")]
        public List<WeaponData> Upgrades;
        public bool AutoUpgrade = false;
        public int RequiredUpgradeValue = 0;
    }

}

