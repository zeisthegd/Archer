using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public T CurrentState { get; protected set; }
    public T PreviousState { get; protected set; }

    public virtual void ChangeState(T newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
    }
}
