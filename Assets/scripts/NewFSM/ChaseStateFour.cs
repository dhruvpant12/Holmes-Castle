using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateFour : State
{
    bool once;
    float timeToWait = 10f;
    float waitingPeriod = 0f;
    public ChaseStateFour(Enemy character, StateMachine stateMachine) : base(character, stateMachine)
    { 
    }

    public override void Enter()
    {
       
        base.Enter();
        character.rend.sharedMaterial = character.material[3];
        once = true;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited Chase State");

    }

 

    public override void LogicUpdate()
    {
        
        Debug.Log("In Chase State");
        waitingPeriod += Time.deltaTime;
        if (waitingPeriod >= timeToWait)
        {
            waitingPeriod = 0;
            stateMachine.ChangeState(character.idleState);

        }
        /*if (once)
        {

            Debug.Log(character.path.path.Count);
            once = false;
        }
        character.chase();
        
        if (character.caughtplayer)
        {
            once = true;
            Debug.Log(character.path.path.Count);
            stateMachine.ChangeState(character.idleState);
            

        }*/

    }

  
    
}
