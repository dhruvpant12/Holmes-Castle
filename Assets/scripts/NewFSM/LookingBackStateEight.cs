using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingBackStateEight : State
{

    float timeToWait = 10f;
    float waitingPeriod = 0f;
    int i = 1;
    public LookingBackStateEight(Enemy character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //character.rend.sharedMaterial = character.material[0];
    }

    public override void Exit()
    {
        Debug.Log("Exited LookingBack State");
        base.Exit();


    }

  
    public override void LogicUpdate()
    {
        Debug.Log("In LookingBack State");
        if (i == 1)
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
