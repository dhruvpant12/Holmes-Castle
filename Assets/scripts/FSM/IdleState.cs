using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IdleState", menuName = "Idle", order =1)]
public class IdleState : AbstractFSMState
{
    public override void UpdateState()
    {
        Debug.Log("Updating Idle State");
    }

    public override bool EnterState()
    {
        base.EnterState();
        Debug.Log("Entered Idle State");
        return true;
    }

    public override bool ExitState()
    {
         base.ExitState();
        Debug.Log("Entered Exit State");
        return true;
    }
}
