using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class RotatedAnimationController : MonoBehaviour
    {
        public AngleInputType AngleInputType;

        private Character _character;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void Update()
        {
            if (AngleInputType == AngleInputType.Mouse)
                HandleMouseAngleInput();
        }

        private void HandleMouseAngleInput()
        {
            Vector2 direction = (CursorManager.Instance.GetMousePosition() - this.transform.position).normalized;
            _character.Animator.SetFloat("AngleX", direction.x);
            _character.Animator.SetFloat("AngleY", direction.y);
        }
    }

    public enum AngleInputType
    {
        Mouse
    }
}