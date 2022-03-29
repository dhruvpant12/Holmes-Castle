using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState2 : State
{
    
    public PatrolState2(Enemy character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        character.rend.sharedMaterial = character.material[1];
        base.Enter();
        
    }

    public override void Exit()
    {
        Debug.Log("Exited Patrol State");
        base.Exit();
        

    }

    
    public override void LogicUpdate()
    {
        Debug.Log("In Patrol State");
        /* waitingPeriod += Time.deltaTime;        
         if (waitingPeriod >= timeToWait)
         {
             waitingPeriod = 0;
             stateMachine.ChangeState(character.howlState);

         }*/
         
        character.partrolaround();
        if(character.changestate)
        {
            character.changestate = false;
            stateMachine.ChangeState(character.howlState);
        }
 

    }


    

    
}
