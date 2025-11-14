using System;
using System.Collections.Generic;
using Game.GameState;
using UnityEngine;

public class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool isTransitioningState = false;
    
    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!isTransitioningState && nextStateKey.Equals((CurrentState.StateKey)))
        {
            CurrentState.UpdateState();
        }
        else if (!isTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState stateKey)
    {
        isTransitioningState = true; 
        CurrentState.ExitState();
        CurrentState = States[stateKey]; 
        CurrentState.EnterState();
        isTransitioningState = false; 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter();
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay();
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit();
    }
}
