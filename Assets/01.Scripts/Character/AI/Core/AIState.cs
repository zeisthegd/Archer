using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [Serializable]
    public class AIState
    {
        public string Name;
        public List<AIAction> Actions;
        public List<DecisionsLogic> Decisions;

        protected AIBrain _brain;

        public virtual void Awake(AIBrain brain)
        {
            _brain = brain;
            AwakeActions();
            AwakeDecisions();
        }

        public virtual void Update()
        {
            UpdateActions();
            UpdateDecisions();
        }

        protected virtual void AwakeActions()
        {
            foreach (AIAction action in Actions)
            {
                action.AwakeComponent(_brain.Character);
            }
        }

        protected virtual void AwakeDecisions()
        {
            foreach (DecisionsLogic decisionsLogic in Decisions)
            {
                decisionsLogic.Decision.AwakeComponent(_brain.Character);
            }
        }


        protected virtual void UpdateActions()
        {
            foreach (AIAction action in Actions)
            {
                action.UpdateComponent();
            }
        }

        protected virtual void UpdateDecisions()
        {
            foreach (DecisionsLogic decisionsLogic in Decisions)
            {
                if (decisionsLogic.Decision.Check())
                {
                    _brain.ChangeState(decisionsLogic.TrueState);
                }
                else
                {
                    _brain.ChangeState(decisionsLogic.FalseState);
                }
            }
        }

        protected virtual void InvokeActionsMethod(string methodName)
        {
            foreach (AIAction action in Actions)
            {
                action.Invoke(methodName, 0);
            }
        }

        protected virtual void InvokeDecisionsMethod(string methodName)
        {
            foreach (DecisionsLogic decisionsLogic in Decisions)
            {
                decisionsLogic.Decision.Invoke(methodName, 0);
            }
        }

        public virtual void Enter()
        {
            InvokeActionsMethod(nameof(AIAction.StateEnter));
            InvokeDecisionsMethod(nameof(AIAction.StateEnter));
        }

        public virtual void Exit()
        {
            InvokeActionsMethod(nameof(AIAction.StateExit));
            InvokeDecisionsMethod(nameof(AIAction.StateExit));
        }
    }

    [Serializable]
    public struct DecisionsLogic
    {
        public AIDecision Decision;
        public string TrueState;
        public string FalseState;
    }
}
