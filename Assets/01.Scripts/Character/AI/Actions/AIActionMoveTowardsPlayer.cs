using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIActionMoveTowardsPlayer : AIAction
    {
        [Header("Base")]
        public float MinDistance = 2;
        public AnimationCurve DistanceToSpeedCurve;

        [Header("Random Movement")]
        public Vector2 MinMaxAngle;
        public Vector2 TimeBeforeChangingDirecion;

        protected GameObject _target;
        protected float _randomAngle;

        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
        }

        public override void UpdateComponent()
        {
            _target = PlayerManager.Instance.Player.gameObject;
            float distanceToPlayer = Vector2.Distance(_target.transform.position, _character.Position);
            if (_target != null && distanceToPlayer > MinDistance)
            {
                Vector2 dirToPlayer = _target.transform.position - _character.Position;
                dirToPlayer = Quaternion.AngleAxis(_randomAngle, Vector3.forward) * dirToPlayer;

                _character.CharacterRun.RunRaw(dirToPlayer);
                _character.Controller.MultiplyVelocity(DistanceToSpeedCurve.Evaluate(distanceToPlayer));
                Debug.DrawRay(_character.Position, _character.Controller.Velocity);
            }
        }

        protected virtual IEnumerator ChangeDirection()
        {
            while (true)
            {
                _randomAngle = Randomizer.RandomNumber(MinMaxAngle.x, MinMaxAngle.y) * Randomizer.RandomBetween(-1, 1);
                yield return new WaitForSeconds(Randomizer.RandomNumber(TimeBeforeChangingDirecion.x, TimeBeforeChangingDirecion.y));
            }
        }

        public override void StateEnter()
        {
            base.StateEnter();
            StartCoroutine(ChangeDirection());
        }

        public override void StateExit()
        {
            base.StateExit();
            StopAllCoroutines();
        }
    }
}
