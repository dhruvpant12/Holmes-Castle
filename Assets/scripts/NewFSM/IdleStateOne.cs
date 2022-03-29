using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateOne : State
{
    float timeToWait = 10f;
    float waitingPeriod = 0f;
    public IdleStateOne(Enemy character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rend.sharedMaterial = character.material[0];
    }

    public override void Exit()
    {
        Debug.Log("Exited Idle State");
        base.Exit();
       
         
    }

   
    public override void LogicUpdate()
    { 
        // The idle state wil be active when the value sent from Holt Winter is 0. IF anything else , it will change to other states.
        Debug.Log("In Idle State");
        if (character.triggerinput == 0)  
        {
            character.transform.Rotate(Vector3.down, 200f * Time.deltaTime);
            waitingPeriod += Time.deltaTime;
            if (waitingPeriod >= timeToWait)
            {
                waitingPeriod = 0;
                stateMachine.ChangeState(character.patrolState);

            }
        }
        else if(character.triggerinput==1)
        {
            stateMachine.ChangeState(character.sprintState);
        }
        else if(character.triggerinput==2)
        {
            stateMachine.ChangeState(character.sleathState);
        }
        else if(character.triggerinput==3)
        {
            stateMachine.ChangeState(character.hideState);
        }
        else if(character.triggerinput==4)
        {
            stateMachine.ChangeState(character.lookbackState);
        }
        else if(character.triggerinput==5)
        {
            stateMachine.ChangeState(character.cornerState);
        }
    }

   
    
}
