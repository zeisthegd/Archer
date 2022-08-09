using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIDecisionWaitForTime : AIDecision
    {

        public float Time;
        public bool IsRandom;
        public Vector2 RandomTime;

        protected bool _value;

        public override bool Check()
        {
            return _value;
        }

        public IEnumerator TimeCounter()
        {
            float timeToWait = IsRandom ? Randomizer.RandomNumber(RandomTime.x, RandomTime.y) : Time;
            yield return new WaitForSeconds(timeToWait);
            _value = true;
        }

        public override void StateEnter()
        {
            base.StateEnter();
            _value = false;
            StartCoroutine(TimeCounter());
        }

        public override void StateExit()
        {
            base.StateExit();
            _value = false;
            StopCoroutine(TimeCounter());
        }
    }
}
