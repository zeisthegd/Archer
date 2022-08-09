using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class StickOnTouch : MonoBehaviour
    {
        [Header("Masks")]
        public LayerMask TargetMask;


        /// <summary>
        /// Deal damage to an object.
        /// </summary>
        public virtual void Stick(GameObject objectToStick)
        {
            if (this.gameObject.activeInHierarchy && objectToStick.activeInHierarchy)
            {
                this.transform.SetParent(objectToStick.transform);
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (this.gameObject.activeInHierarchy && TargetMask.Contains(col.gameObject.layer))
            {
                Stick(col.gameObject);
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
