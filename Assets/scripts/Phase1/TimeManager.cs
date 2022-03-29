using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    
   
     string path;

    public StreamWriter writer;

    public Holtz evaluator;




   public int iterationTimer; // array row will be updated every 30 sec.
    public int Holttrigger=60;

    public bool resetTrigger;
     
    
   public  List<float[]> datafile = new List<float[]>(); // Saving data in a float array at every interval of iterationTimer.
    string[] statename = new string[5];    // Holding the player state .
   


    public float timeOfRunning,timeOfSleath,timeOfHiding,timeOfLookingBack,timeOfCorners; //Variables holding the values of input time duration which will be passed into the float array.
      /*
     [0] is for sprinting
     [1] is for sleath
     [2] is for hiding
     [3] is for looking back
     [4] is for corners.
     */

    //Player State
    public string playerstate;

   // Timestamp collection.
    float beginTime;
    int beginTimeToInt;

    //Loop control.
    bool firstframe;

    //display variables
    public Text currentState; // player state.
    public Text time; // state time
    public Text timer; // game timer
    GameObject a;
    GameObject b;
    GameObject c;
   
   

    // Start is called before the first frame update
    void Start()
    {
        
        iterationTimer = 30;
        resetTrigger = false;
        beginTime = Time.time;
        firstframe = true;
        statename[0] = "sprint";
        statename[1] = "sleath";
        statename[2] = "hiding";
        statename[3] = "lookingback";
        statename[4] = "corner";

        a = GameObject.FindGameObjectWithTag("playerstate");            
        b = GameObject.FindGameObjectWithTag("statetimer");
        c = GameObject.FindGameObjectWithTag("gametimer");
        currentState = a.GetComponent<Text>();
        time = b.GetComponent<Text>();
        timer = c.GetComponent<Text>();
        evaluator = gameObject.AddComponent<Holtz>();
        
        path = Application.dataPath + "/datatest.txt";
        if (!File.Exists(path))
            File.WriteAllText(path," \n\n");

        writer = new StreamWriter(path);
       
       

    }

    // Update is called once per frame
    void Update()
    {
       
        beginTime += Time.deltaTime;
        beginTimeToInt = (int)beginTime;

        //display Player state through displaystate function.
        
         currentState.text = playerstate ;  // Player State.

         timer.text = beginTimeToInt.ToString();   //Game Timer

         time.text = statename[0] + "   " +  timeOfRunning.ToString()   + "      " +    // State Timer
                     statename[1] + "   " + timeOfSleath.ToString()     + "      " +
                     statename[2] + "   " + timeOfHiding.ToString()     + "      " +
                     statename[3] + "   " + timeOfLookingBack.ToString()+ "      " +
                     statename[4] + "   " + timeOfCorners.ToString();   



        // Debug.Log(beginTmeToInt);
        if ((beginTimeToInt % iterationTimer) == 0 && beginTimeToInt != 0 && firstframe)
        {
            int counter=1;
           
            firstframe = false;
            Debug.Log("Saving input values in list");

          
            datafile.Add(new float[] { timeOfRunning, timeOfSleath, timeOfHiding, timeOfLookingBack, timeOfCorners });
         //   WriteTOFIle(timeOfRunning, timeOfSleath, timeOfHiding, timeOfLookingBack, timeOfCorners);
          
            resetTrigger = true;
           

            foreach (float[] i in datafile)
            {
                Debug.Log("Display Row  " + counter);
                counter++;
                for(int j=0;j<i.Length;j++)
                {
                    Debug.Log(i[j]);
                }
            }
            Debug.Log("finished printing");

            if ((beginTimeToInt % Holttrigger) == 0 && beginTimeToInt != 0 )
            {
                int k = 0;
                foreach (float[] i in datafile)
                {

                    for (int j = 0; j < i.Length; j++)
                    {
                        evaluator.Holtzdataset[k, j] = i[j];
                    }

                    k++;
                }

                evaluator.TriggerValue();

                /*Debug.Log("printing holtz");
                for (int z = 0; z < k; z++)
                {
                    for (int o = 0; o < 5; o++)
                    {
                        Debug.Log(evaluator.Holtzdataset[z, o]);
                    }
                }*/
            }
        }
        else
        {
            if(beginTimeToInt % iterationTimer != 0)
             firstframe = true;
            
        }

      
    }


    
}
