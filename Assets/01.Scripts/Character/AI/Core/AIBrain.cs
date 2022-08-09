using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using NaughtyAttributes;
namespace Penwyn.Game
{
    public class AIBrain : MonoBehaviour
    {
        public List<AIState> States;
        public AIState CurrentState;
        public bool Enabled = true;
        [ReadOnly] public string CurrentStateName;

        protected Character _character;

        public virtual void Awake()
        {
            _character = GetComponent<Character>();
            AwakeStates();
            if (States.Count > 0)
                CurrentState = States[0];
        }

        public virtual void Update()
        {
            if (Enabled)
            {
                CurrentState.Update();
            }
        }

        /// <summary>
        /// If the input state name is not null
        /// and the new state is not the old state,
        /// change to the new state.
        /// </summary>
        public void ChangeState(string stateName)
        {
            if (stateName != "")
            {
                AIState newState = States.Find(x => x.Name == stateName);
                if (newState != null)
                    ChangeState(newState);
                else
                    Debug.LogWarning($"State {stateName} was not found.");
            }
        }

        /// <summary>
        /// Exit the old state, change into the new state.
        /// </summary>
        public void ChangeState(AIState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
            CurrentStateName = CurrentState.Name;
        }

        /// <summary>
        /// Wake every states up.
        /// </summary>
        public virtual void AwakeStates()
        {
            for (int i = 0; i < States.Count; i++)
            {
                States[i].Awake(this);
            }
        }

        public virtual void OnEnable()
        {
            if (Enabled)
                CurrentState.Enter();
        }

        public virtual void OnDisable()
        {
            if (Enabled)
                CurrentState.Exit();
            Enabled = false;
        }

        public Character Character => _character;
    }
}

