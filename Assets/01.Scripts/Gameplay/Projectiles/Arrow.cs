using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;
namespace Penwyn.Game
{

    public class Arrow : Projectile
    {
        public override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            if (DamageOnTouch.TargetMask.Contains(col.gameObject.layer))
                Controller.SetVelocity(Vector2.zero);
        }
    }
}
