using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    float time1; //Time action starts
    float time2; //Time actions ends
    
    float begintime;
    int   begintimeInt;

    public float sprintTime ;
    public float sleathTime ; 
    public float hidingTime ;
    public float lookingbackTime ;
    public float cornerTime ;

    // Variables to hold time duration for each player state.
   public float sprintentry;
    float sprintexit;
    float sleathentry;
    float sleathexit;
    float hidingentry;
    float hidingexit;
    float lookingbackentry;
    float lookingbackexit;
    float cornerentry;
    float cornerexit;

    int inputControl; // Counter to control flow of enter and exit state.
    
    public CharacterController controller;
    public string playerState; // Player state like sprint, sleath,hiding, corner and looking back


    TimeManager agent; // Object of Timemanager Class.
    LookAround lookback;

    //Movement
    public float playerspeed ;
    public float playernormalspeed = 4f;
    public float playerRunningSpeed = 8f;
    public float playerSleathSpeed = 2.5f;
    public bool canMove; // Counter to stop movement during hiding , corner and looking back state.

    //Input variables for character movement.
    float xdirection;
    float zdirection;

    //Falling & jump command.
    Vector3 verticalVelocity;
    float gravity = -19.6f;
    float jumpHeight = 1.5f;

    //Grounded check
    public Transform groundcheck;
    public float grounDistance = 0.4f;
    public LayerMask groundMask;
    bool isOnGround;

    //Crouching
    public float crouchingHeight;

    float correction;
    bool firstframe;
    void Start()
    {
        firstframe = true;
        begintime = Time.time;
        playerspeed = playernormalspeed;
        playerState = "Idle";
        canMove = true;
       
        agent = gameObject.AddComponent<TimeManager>();
       lookback = gameObject.AddComponent<LookAround>();
        inputControl = 1;

        sprintTime = 0;
        sleathTime = 0;
         hidingTime = 0;
        lookingbackTime = 0;
        cornerTime = 0;

}

    // Update is called once per frame
    void Update()
    {
        begintime += Time.deltaTime;
        begintimeInt = (int)begintime;
        if(begintimeInt % agent.iterationTimer == 0 && begintimeInt != 0 && firstframe)
        {
            firstframe = false;
            switch(playerState)
            {
                case "Running":

                    sprintexit = begintimeInt;
                    correction = sprintentry;
                    sprintentry = begintimeInt;
                    sprintTime += sprintexit - correction;
                    agent.timeOfRunning = sprintTime;
                    break;

                case "Sleath":

                    sleathexit = begintimeInt;
                    correction = sleathentry;
                    sleathentry = begintimeInt;
                    sleathTime += sleathexit - correction;
                    agent.timeOfSleath = sleathTime;
                    break;

                case "Hiding":

                    hidingexit = begintimeInt;
                    correction = hidingentry;
                    hidingentry = begintimeInt;
                    hidingTime += hidingexit - correction;
                    agent.timeOfHiding = hidingTime;
                    break;

                case "Looking Back":

                    lookingbackexit = begintimeInt;
                    correction = lookingbackentry;
                    lookingbackentry = begintimeInt;
                    lookingbackTime += lookingbackexit - correction;
                    agent.timeOfLookingBack = lookingbackTime;
                    break;

                case "Checking around corners":

                    cornerexit = begintimeInt;
                    correction = cornerentry;
                    cornerentry = begintimeInt;
                    cornerTime += cornerexit - correction;
                    agent.timeOfCorners = cornerTime;
                    break;


                default: break;
            }
               
        }
        else
        {
            if (begintimeInt % agent.iterationTimer != 0)
                firstframe = true;
        }
        
       
        //Checking if character is on ground.
        isOnGround = Physics.CheckSphere(groundcheck.position, grounDistance, groundMask);

        if (canMove) // If true , character can move.
        {
            //Sprint command.
            //Entry level for Sprint state.
            if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround && inputControl == 1)
            {
                
                sprintentry = Time.time;
                playerState = "Running";
                playerspeed = playerRunningSpeed;
                inputControl = 2;
                
               

            }
            //Exit level for Sprint state.
            if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround && inputControl==2 && playerState == "Running")
            {
                inputControl = 1;
                sprintexit = Time.time;
                sprintTime += sprintexit - sprintentry;
               // sprintTime += time2 - time1;
                agent.timeOfRunning = sprintTime;                
                playerState = "exited running";
                playerspeed = playernormalspeed;
            }
            

            //Sleath command
            //Entry level for Sleath state.
            if (Input.GetKeyDown(KeyCode.LeftControl) && isOnGround && inputControl==1)
            {
                sleathentry = Time.time;
                playerState = "Sleath";
                playerspeed = playerSleathSpeed;
                inputControl = 2;
                
            }
            //Exit level for Sleath state.
            if (Input.GetKeyUp(KeyCode.LeftControl) && isOnGround && inputControl == 2 && playerState == "Sleath")
            {
                inputControl = 1;
                sleathexit = Time.time;
                sleathTime += sleathexit - sleathentry;
                agent.timeOfSleath = sleathTime;               
                playerState = "exited sleath";
                playerspeed = playernormalspeed;
            }

           

            //Moving the character.
            xdirection = Input.GetAxis("Horizontal");
            zdirection = Input.GetAxis("Vertical");
            Vector3 playerMovement = transform.right * xdirection + transform.forward * zdirection;
            controller.Move(playerMovement * playerspeed * Time.deltaTime);

        }

        //hide in closet.
        //Entry level for Hiding state.
        if (Input.GetKeyDown(KeyCode.E) && inputControl == 1 )
        {
            hidingentry = Time.time;
            canMove = false;
            
            playerState = "Hiding";
            inputControl = 2;
            

        }
        //Exit level for Hiding state.
        if (Input.GetKeyUp(KeyCode.E) && inputControl == 2 && playerState == "Hiding")
        {
            hidingexit = Time.time;
            hidingTime += hidingexit-hidingentry;
            agent.timeOfHiding = hidingTime;         
            canMove = true;

            playerState = "exited hiding";
            inputControl = 1;
        }



        //looking back
        //Entry level for looking back state.
        if (Input.GetKeyDown(KeyCode.Q) && inputControl == 1 )
        {
            lookingbackentry = Time.time;
            canMove = false;

            lookback.lookback();
           
            playerState = "Looking Back";
            inputControl = 2;
           

        }
        //Exit level for looking back state.
        if (Input.GetKeyUp(KeyCode.Q) && inputControl == 2 && playerState == "Looking Back")
        {
            lookingbackexit = Time.time;
            lookingbackTime += lookingbackexit-lookingbackentry;
            agent.timeOfLookingBack = lookingbackTime;           
            canMove = true;
            lookback.lookback();
           
            playerState = "exited looking back";
            inputControl = 1;
        }



        //looking around corners.
        //Entry level for Corner state.
        if (Input.GetKeyDown(KeyCode.R) && inputControl == 1)
        {
            cornerentry = Time.time;
            canMove = false;
            playerState = "Checking around corners";
            inputControl = 2;
           

        }
        //Exit level for Corner state.
        if (Input.GetKeyUp(KeyCode.R) && inputControl == 2 && playerState == "Checking around corners")
        {
            cornerexit = Time.time;
            cornerTime += cornerexit-cornerentry;
            agent.timeOfCorners = cornerTime;           
            canMove = true;

            playerState = "exited corner";
            inputControl = 1;
        }


        agent.playerstate = playerState; //Passing player current state with entry and exit levels.

        if (agent.resetTrigger) // Reset variable with 0 when new iteration starts. 
        {
            Debug.Log("zero value entered.");
            sprintTime = 0;
            sleathTime = 0;
            hidingTime = 0;
            lookingbackTime = 0;
            cornerTime = 0;
            agent.timeOfRunning = 0;
            agent.timeOfSleath = 0;
            agent.timeOfHiding = 0;
            agent.timeOfCorners = 0;
            agent.timeOfLookingBack = 0;
            agent.resetTrigger = false;
        }


        /* 

        */



        if (isOnGround == true && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;

        }

        //Jumping command.
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        //Falling command.
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
       

    }

    
    void OnApplicationQuit()
    {
         

        foreach (float[] i in agent.datafile)
        {

            for (int j = 0; j < i.Length; j++)
            {
                if(j==4)
                    agent.writer.Write(i[j]);
                else
                agent.writer.Write(i[j] + ",");


            }
            agent.writer.WriteLine();
        }

        agent.writer.Flush();
        agent.writer.Close();
    }


}
