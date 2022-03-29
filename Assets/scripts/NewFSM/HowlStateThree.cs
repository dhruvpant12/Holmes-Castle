using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlStateThree : State
{
    float timeToWait = 3f;
    float waitingPeriod = 0f;
    int i = 1;
     
    public HowlStateThree(Enemy character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rend.sharedMaterial = character.material[2];

    }

    public override void Exit()
    {
        i = 1;
        Debug.Log("Exited Howl State");
        base.Exit();
        

    }

  

    public override void LogicUpdate()
    {
        Debug.Log("In Howl Start");

        if(i==1)
        {
            character.audiosource.Play();
            i = 2;
        }
        waitingPeriod += Time.deltaTime;
        if (waitingPeriod >= timeToWait)
        {
            waitingPeriod = 0;
            stateMachine.ChangeState(character.idleState);
             
        }

        
    }


   
}
