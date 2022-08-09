using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class AIAction : AIComponent
    {
        protected bool _isOperatable;
        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
        }
        public override void UpdateComponent() { base.UpdateComponent(); }
        public override void StateEnter() { base.StateEnter(); }
        public override void StateExit() { base.StateExit(); }

    }
}

