using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class CharacterFriction : CharacterAbility
    {
        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
        }

        public override void UpdateAbility()
        {
            base.UpdateAbility();
        }

        public override void FixedUpdateAbility()
        {
            ApplyFriction();
        }

        protected virtual void ApplyFriction()
        {
            if (InputReader.Instance.MoveInput.magnitude < 0.01F && _controller.Velocity.magnitude > 0)
            {
                Vector2 amount = _controller.Body2D.velocity.normalized * -1F;
                amount *= _controller.Settings.Friction;
                _controller.AddForce(amount, ForceMode2D.Force);
                Debug.DrawRay(_character.Position, amount, Color.yellow);
            }
        }

    }
}
