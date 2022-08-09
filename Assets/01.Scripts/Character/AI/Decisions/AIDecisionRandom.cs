using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIDecisionRandom : AIDecision
    {
        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
        }

        public override bool Check()
        {
            return Randomizer.RandomBetween(0, 1) == 1 ? true : false;
        }

        public override void StateEnter()
        {
            base.StateEnter();
        }

        public override void StateExit()
        {
            base.StateExit();
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();
        }
    }
}

