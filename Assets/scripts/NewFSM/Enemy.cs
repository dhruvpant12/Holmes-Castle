using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StateMachine movementSM;
    public IdleStateOne idleState;
    public PatrolState2 patrolState;
    public HowlStateThree howlState;
    public ChaseStateFour chaseState;
    public ReactToSprintStateFive sprintState;
    public SleathStateSix sleathState;
    public HideStateSeven hideState;
    public LookingBackStateEight lookbackState;
    public CornerStateNine cornerState;

    public int triggerinput;
    public Transform target;
    public Transform playerReset;
    public int pathindex;

   public  Gridd path;
    
    public Transform partrolpointOne;
    public Transform partrolpointTwo;
    bool pointOne, pointTwo;
    public bool changestate;

    public Material[] material;
    public Renderer rend;

    public bool chaseStatus;
    public bool caughtplayer;

    public AudioSource audiosource;
    public Vector3 enemypos;






    private void Awake()
    {
        triggerinput = 0;
        path = GetComponent<Gridd>();
        pointOne = true;
        pointTwo = false;
        
    }
    
    
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        chaseStatus = true;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        movementSM = new StateMachine();

        idleState = new IdleStateOne(this, movementSM);
        patrolState = new PatrolState2(this, movementSM);
        howlState = new HowlStateThree(this, movementSM);
        chaseState = new ChaseStateFour(this, movementSM);
        sprintState = new ReactToSprintStateFive(this, movementSM);
        sleathState = new SleathStateSix(this, movementSM);
        hideState = new HideStateSeven(this, movementSM);
        lookbackState = new LookingBackStateEight(this, movementSM);
        cornerState = new CornerStateNine(this, movementSM);

        movementSM.Initialize(idleState);
    }

    private void Update()
    {               
        movementSM.CurrentState.LogicUpdate();         
    }

   

    public void chase()
    {


        //destinationnode=character.path.path[1].nodeposition

        transform.position = Vector3.MoveTowards(transform.position, target.position, 8f * Time.deltaTime);
            if (transform.position == target.position)
            {
                pathindex++;
                if (pathindex >= path.path.Count-1)
                {
                pathindex = 0;
                caughtplayer = true;
                target.transform.position = playerReset.position;
                     return;
                }
                else
                {
                
                    enemypos = path.path[pathindex].nodeposition;
                }
            }
           
                
                   
                          


    }
        
    public void partrolaround()
    {
        if (pointOne == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, partrolpointOne.position, 6f * Time.deltaTime);
            if(transform.position==partrolpointOne.position)
            {
                pointOne = false;
                pointTwo = true;
                changestate = true;
            }
        }

        if (pointTwo == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, partrolpointTwo.position, 6f * Time.deltaTime);
            if (transform.position == partrolpointTwo.position)
            {
                pointOne = true;
                pointTwo = false;
                changestate = true;
            }
        }

    }


}
