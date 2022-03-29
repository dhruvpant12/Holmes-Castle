using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine  
{
    public State CurrentState { get; private set; } //This will store the reference to the current active state of the state machine.

    public void Initialize(State startingState) // This is called the first time FSM assigns a state.
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState) //This function is responsible for changing the states. First current state is exited and then a new state is assigned and passed to Enter function to validate the transition.
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
