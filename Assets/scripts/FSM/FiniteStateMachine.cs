using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{

    [SerializeField] AbstractFSMState startingState;

    AbstractFSMState currentstate;

    private void Awake()
    {
        currentstate = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(startingState != null)
        {
            EnterState(startingState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentstate!=null)
        {
            currentstate.UpdateState();
        }
    }

    #region State Management

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
            return;

        currentstate = nextState;
        currentstate.EnterState();
    }
    #endregion
}
