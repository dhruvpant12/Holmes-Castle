using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//All AI behavious states will inherit from this class.
public abstract class State //Base Class.
{
    protected Enemy character;
    protected StateMachine stateMachine;

    protected State(Enemy character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    protected void DisplayOnUI()
    {
       
    }

    public virtual void Enter() //This will call Entry on new active state.
    {
       
    }

    

    public virtual void LogicUpdate()
    {

    }

  

    public virtual void Exit() //This will call Exit on the current State.
    {

    }
}
