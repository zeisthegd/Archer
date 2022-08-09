using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class DamageOnTouch : MonoBehaviour
    {
        [Header("Masks")]
        public LayerMask TargetMask;
        [HorizontalLine]

        [Header("Damage To Target")]
        public float DamageDeal = 1;

        [Header("Feedbacks")]
        public Feedbacks HitFeedbacks;

        /// <summary>
        /// Deal damage to an object.
        /// </summary>
        public virtual void DealDamage(GameObject gObject)
        {
            if (this.gameObject.activeInHierarchy)
            {
                Health objectHealth = gObject.FindComponent<Health>();
                objectHealth?.Take(DamageDeal);
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            HitFeedbacks?.PlayFeedbacks();
            if (this.gameObject.activeInHierarchy && TargetMask.Contains(col.gameObject.layer))
            {
                DealDamage(col.gameObject);
            }
        }


        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }
    }
}
