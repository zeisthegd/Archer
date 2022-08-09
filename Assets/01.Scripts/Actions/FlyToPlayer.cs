using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
namespace Penwyn.Tools
{
    [RequireComponent(typeof(MovementController))]
    public class FlyToPlayer : MonoBehaviour
    {
        public float Speed = 10;
        public float WaitTillFly = 0.5F;
        protected bool _flyStarted = false;

        protected MovementController _controller;

        protected virtual void Awake()
        {
            _controller = GetComponent<MovementController>();
        }

        protected virtual void OnEnable()
        {
            StopCoroutine(StartFlying());
            StartCoroutine(StartFlying());
        }

        protected virtual void OnDisable()
        {
            StopFlying();
        }

        protected virtual void Update()
        {
            if (_flyStarted)
            {
                _controller.SetVelocity((PlayerManager.Instance.Player.Position - this.transform.position).normalized * Speed);
                Debug.DrawRay(this.transform.position, (PlayerManager.Instance.Player.Position - this.transform.position).normalized * Speed);
            }
        }

        protected virtual IEnumerator StartFlying()
        {
            yield return new WaitForSeconds(WaitTillFly);
            _flyStarted = true;
        }

        protected virtual void StopFlying()
        {
            StopCoroutine(StartFlying());
            _flyStarted = false;
        }
    }
}

