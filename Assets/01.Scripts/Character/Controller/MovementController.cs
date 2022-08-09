using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class MovementController : MonoBehaviour
    {

        [Header("Feedbacks")]
        public ParticleSystem Dust;

        protected Rigidbody2D _body;
        protected Collider2D _collider;

        private StateMachine<ControllerState> _states;

        protected virtual void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public virtual void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
        {
            _body.AddForce(force, mode);
        }

        public virtual void AddPosition(Vector3 positionAddition)
        {
            transform.position += positionAddition;
        }

        public virtual void MultiplyVelocity(float multiplier)
        {
            MultiplyVelocity(Vector2.one * multiplier);
        }

        public virtual void MultiplyVelocity(Vector2 multiplier)
        {
            SetVelocity(_body.velocity * multiplier);
        }

        public virtual void SetVelocity(Vector2 newVelocity)
        {
            _body.velocity = newVelocity;
        }

        #region Physics Check

        public RaycastHit2D RayCast(Collider2D origin, Vector2 direction, float distance, LayerMask mask)
        {
            float extentsAxis = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? origin.bounds.extents.x : origin.bounds.extents.y;
            RaycastHit2D hit = Physics2D.Raycast(origin.bounds.center, direction, extentsAxis + distance, mask);
            return hit;
        }

        #endregion

        void OnEnable()
        {

        }

        void OnDisable()
        {

        }


        public Rigidbody2D Body2D { get => _body; }
        public Vector3 Velocity { get => _body.velocity; }
        public StateMachine<ControllerState> States { get => _states; }
    }

}
