using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class ProjectileWeapon : Weapon
    {
        protected ObjectPooler _projectilePooler;

        protected override void UseWeapon()
        {
            base.UseWeapon();
            StartCoroutine(IterationCoroutine());
        }


        protected virtual IEnumerator IterationCoroutine()
        {
            if (_weaponAim)
                _weaponAim.enabled = false;
            for (int i = 0; i < CurrentData.Iteration; i++)
            {
                StartCoroutine(UseWeaponCoroutine());
                yield return new WaitForSeconds(CurrentData.DelayBetweenIterations);
            }
            if (_weaponAim)
                _weaponAim.enabled = true;
            if (_weaponAutoAim != null && _weaponAutoAim.enabled == false)
                _weaponAutoAim.enabled = true;
            StartCooldown();
        }

        protected virtual IEnumerator UseWeaponCoroutine()
        {
            float projectileStep = GetProjectileStep();

            gameObject.RotateZ(CurrentData.Angle / 2F);
            for (int i = 0; i < CurrentData.BulletPerShot; i++)
            {
                SpawnProjectile();
                if (CurrentData.BulletPerShot > 1)
                {
                    if (CurrentData.DelayBetweenBullets > 0)
                        yield return new WaitForSeconds(CurrentData.DelayBetweenBullets);
                    gameObject.RotateZ(-projectileStep);
                }
            }
        }

        /// <summary>
        /// Create a projectile, direction is based on the weapon's rotation.
        /// </summary>
        public virtual void SpawnProjectile()
        {
            Projectile projectile = _projectilePooler.PullOneObject().GetComponent<Projectile>();
            projectile.transform.position = this.transform.position;
            projectile.transform.rotation = this.transform.rotation;
            projectile.gameObject.SetActive(true);
            projectile.FlyTowards((transform.rotation * Vector3.right));
        }

        /// <summary>
        /// Angle distance of each projectile.
        /// </summary>
        protected virtual float GetProjectileStep()
        {
            if (CurrentData.BulletPerShot != 0)
                return 1F * CurrentData.Angle / CurrentData.BulletPerShot;
            return 0;
        }

        public override void LoadWeapon(WeaponData data)
        {
            base.LoadWeapon(data);
            CurrentData.Projectile.DamageOnTouch.DamageDeal = CurrentData.Damage;

            CreateNewPool();
        }

        public virtual void CreateNewPool()
        {
            if (_projectilePooler.NoPoolFound())
            {
                _projectilePooler.ObjectToPool = CurrentData.Projectile.gameObject;
                _projectilePooler.ClearPool();
                _projectilePooler.Init();
            }
        }

        public override void GetComponents()
        {
            base.GetComponents();
            _projectilePooler = GetComponent<ObjectPooler>();
        }
    }
}
