using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holtz : MonoBehaviour
{
    int indexkeeper = 5;
    public int colNum = 80;
    public float[,] Holtzdataset;
    float[] sprintarray = new float[80];
    float[] sleatharray = new float[80];
    float[] hidearray = new float[80];
    float[] lookingbackarray = new float[80];
    float[] cornerarray = new float[80];
    float sprintsum=0;
    float sleathsum=0;
    float hidesum=0;
    float lookingbacksum=0;
    float cornersum=0;
    float sprintavg;
    float sleathavg;
    float hideavg;
    float lookingabckavg;
    float corneravg;
    float indexavg;
    int indexsum;
    float sprintTrend;
    float sleahtTrend;
    float hideTrend;
    float lookingbackTrend;
    float cornerTrend;
    float  g = 0;
    float SprintSigma;
    float SleathSigma;
    float HideSigma;
    float LookingSigma;
    float CornerSigma;
    float sumofSprintSigma;
    float sumofSleathSigma;
    float sumofHideSigma;
    float sumofLookingSimga;
    float sumofCornerSigma;
    float sumofX;
    float sumof2X; //square of X
    float sumofSprint;
    float sumofSleath;
    float sumofHide;
    float sumofLooking;
    float sumofCorner;
    float sumOfXSprint;
    float sumofXSleath;
    float sumofXhide;
    float sumofXlooking;
    float sumofXcorner;
    float CforSprint;
    float CforSleath;
    float CforHide;
    float CforLooking;
    float CforCorner;

    // Start is called before the first frame update
    void Start()
    {
        Holtzdataset = new float[5, colNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerValue()
    {
        for(int i=0; i<indexkeeper; i++)
        {
            int j = 0;
            sprintarray[i]=Holtzdataset[i, j];
            sleatharray[i]= Holtzdataset[i, j+1];
            hidearray[i] = Holtzdataset[i, j+2];
            lookingbackarray[i] = Holtzdataset[i, j+3];
            cornerarray[i] = Holtzdataset[i, j+4];
        }
        
        for(int i=0;i<indexkeeper;i++)
        {
            sprintsum += sprintarray[i];
            sleathsum += sleatharray[i];
            hidesum += hidearray[i];
            lookingbacksum += lookingbackarray[i];
            cornersum += cornerarray[i];

        }

        sprintavg = sprintsum / indexkeeper;
        sleathavg = sleathsum / indexkeeper;
        hideavg = hidesum / indexkeeper;
        lookingabckavg = lookingbacksum / indexkeeper;
        corneravg = cornersum / indexkeeper;

        for(int i=1; i<=indexkeeper; i++)
        {

            indexsum += i;
        }

        indexavg = indexsum / indexkeeper;

        for(int i=0; i<indexkeeper; i++)
        {

            float k = (i + 1) - indexavg; // x-xAvg
            g = g + (k * k); // x-xAvg square
            SprintSigma = (sprintarray[i] - sprintavg);
            sumofSprintSigma += k*SprintSigma; // SUm of (x-xAvg)(y-yAvg)            
            SleathSigma = sleatharray[i] - sleathavg;
            sumofSleathSigma += k+SleathSigma;
            HideSigma = hidearray[i] - hideavg;
            sumofHideSigma += k+HideSigma;
            LookingSigma = lookingbackarray[i] - lookingabckavg;
            sumofLookingSimga += k+LookingSigma;
            CornerSigma = cornerarray[i] - corneravg;
            sumofCornerSigma += k+CornerSigma;
              
             

        }

        // This will provide slope for each player behaviour.
        sprintTrend = sumofSprintSigma / g;
        sleahtTrend = sumofSleathSigma / g;
        hideTrend = sumofHideSigma / g;
        lookingbackTrend = sumofLookingSimga / g;
        cornerTrend = sumofCornerSigma / g;

        for(int i=0;i <indexkeeper; i++)
        {
            sumofX += (i + 1);
            sumof2X += sumofX * sumofX;
            sumofSprint += sprintarray[i];
            sumofSleath += sleatharray[i];
            sumofHide += hidearray[i];
            sumofLooking += lookingbackarray[i];
            sumofCorner += cornerarray[i];
            sumOfXSprint += (i + 1) * sprintarray[i];
            sumofXSleath += (i + 1) * sleatharray[i];
            sumofXhide += (i + 1) * hidearray[i];
            sumofXlooking += (i + 1) * lookingbackarray[i];
            sumofXcorner += (i + 1) * lookingbackarray[i];

        }

        //This will provide the Y intercept for each player behaviour.
        CforSprint = ((sumof2X * sumofSprint) - (sumofX * sumOfXSprint)) / ((sumof2X * indexkeeper) - (sumofX * sumofX));
        CforSleath = ((sumof2X * sumofSleath) - (sumofX * sumofXSleath)) / ((sumof2X * indexkeeper) - (sumofX * sumofX));
        CforHide = ((sumof2X * sumofHide) - (sumofX * sumofXhide)) / ((sumof2X * indexkeeper) - (sumofX * sumofX));
        CforLooking = ((sumof2X * sumofLooking) - (sumofX * sumofXlooking)) / ((sumof2X * indexkeeper) - (sumofX * sumofX));
        CforCorner = ((sumof2X * sumofCorner) - (sumofX * sumofXcorner)) / ((sumof2X * indexkeeper) - (sumofX * sumofX));




    }
}
