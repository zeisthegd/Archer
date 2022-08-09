using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class AIComponent : MonoBehaviour
    {
        protected Character _character;
        public virtual void AwakeComponent(Character character)
        {
            _character = character;
        }
        public virtual void UpdateComponent() { }
        public virtual void StateEnter() { }
        public virtual void StateExit() { }
    }
}

