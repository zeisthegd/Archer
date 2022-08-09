using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class WeaponAim : MonoBehaviour
    {
        public WeaponAimType AimType;
        protected Weapon _weapon;


        protected virtual void Start()
        {
            _weapon = GetComponent<Weapon>();
        }

        protected virtual void Update()
        {
            switch (AimType)
            {
                case WeaponAimType.Mouse:
                    MouseAim();
                    break;
                case WeaponAimType.ForwardMovement:
                    ForwardMovementAim();
                    break;
                default:
                    break;
            }
        }

        protected virtual void MouseAim()
        {
            Vector3 dirToMouse = CursorUtility.GetMousePosition() - _weapon.transform.position;
            _weapon.transform.right = dirToMouse;
        }

        protected virtual void ForwardMovementAim()
        {
            if (InputReader.Instance.MoveInput.magnitude > 0)
                _weapon.transform.right = InputReader.Instance.MoveInput;
        }
    }

    public enum WeaponAimType
    {
        Mouse,
        ForwardMovement,
        Script
    }
}

