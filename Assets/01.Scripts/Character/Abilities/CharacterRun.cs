using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class CharacterRun : CharacterAbility
    {
        [Header("Speed")]
        public float RunSpeed = 5;
        public ControlType Type;
        [Header("Feedbacks")]
        public ParticleSystem Dust;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
        }

        public override void UpdateAbility()
        {
            base.UpdateAbility();
            Vector3.ClampMagnitude(_controller.Velocity, RunSpeed);
        }

        public override void FixedUpdateAbility()
        {
            base.FixedUpdateAbility();
            if (Type == ControlType.PlayerInput)
                RunRaw(InputReader.Instance.MoveInput);
            DustHandling();

        }

        public void RunRaw(Vector2 direction)
        {
            _controller.SetVelocity(direction.normalized * RunSpeed);
        }

        protected virtual void DustHandling()
        {
            if (Dust != null)
            {
                if (_controller.Velocity.magnitude > 0)
                {
                    if (!Dust.isPlaying)
                        Dust.Play();
                }
                else
                {
                    Dust.Stop();
                }
            }
        }

        public override void ConnectEvents()
        {
            base.ConnectEvents();
        }

        public override void DisconnectEvents()
        {
            base.DisconnectEvents();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public enum ControlType
        {
            PlayerInput,
            Script
        }
    }
}
