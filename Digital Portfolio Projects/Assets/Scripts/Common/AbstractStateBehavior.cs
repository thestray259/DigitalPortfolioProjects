using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStateBehavior<StateType> : MonoBehaviour where StateType : System.Enum
{
    [field: SerializeField]

    public StateType State
    {
        get;
        protected set; 
    }

    public delegate void StateChangedEvent(StateType OldState, StateType NewState);
    //public StateChangeEvent OnStateChange; // need to make StateChangeEvent class? 

    public virtual void ChangeState(StateType NewState)
    {
        //OnStateChange?.Invoke(State, NewState);
        State = NewState; 
    }
}
