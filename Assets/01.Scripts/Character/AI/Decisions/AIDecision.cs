using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class AIDecision : AIComponent
    {
        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
        }
        public virtual bool Check() { return false; }
        public override void StateEnter() { base.StateEnter(); }
        public override void StateExit() { base.StateExit(); }
    }
}

