using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FiniteStateMachine))]
public class NPC : MonoBehaviour
{
    FiniteStateMachine finiteStateMachine;
    
    private void Awake()
    {
        finiteStateMachine = this.GetComponent<FiniteStateMachine>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
