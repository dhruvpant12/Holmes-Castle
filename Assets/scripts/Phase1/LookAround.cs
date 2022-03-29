using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
     Transform player;
    GameObject maincamera;
    float mouseX;
    float mouseY;
    public float mouseTurnRate = 60f;
    float lookUp = 0f;
    bool lookbackcheck;
    float previouspostion;
    Quaternion previouspos;
    GameObject characterplayer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookbackcheck = true;
        characterplayer = GameObject.FindGameObjectWithTag("Player");
        player = characterplayer.GetComponent<Transform>();
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
         
    }

    // Update is called once per frame
    void Update()   
    {
        // Moving the head.
        mouseX = Input.GetAxis("Mouse X")*mouseTurnRate*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseTurnRate * Time.deltaTime;

        lookUp -= mouseY; //Every frame , lookUp is updated with the negative value of mouseY. Negative value bacuase positive value is flipping the camera the other way.
        lookUp = Mathf.Clamp(lookUp, -60f, 60f); // This will stop the camera from moving to the back of the head on the vertical plane.Prevents overrotation and looking behind the player.

        maincamera.transform.localRotation = Quaternion.Euler(lookUp, 0f, 0f); //Rotation is happening along the x axis. Only affects the camera.

        player.transform.Rotate(Vector3.up * mouseX);       


    }

    public void lookback()
    {
         
        if(lookbackcheck)
        {
            previouspos = transform.rotation;
            player.rotation = Quaternion.Euler(0, transform.position.y - 150f, 0);
           // transform.localRotation = Quaternion.Euler(0, transform.position.y - 150f, 0);
            lookbackcheck = false;
            
        }
        else
        {
            player.rotation = Quaternion.Euler(previouspos.x, previouspos.y, previouspos.z);
            lookbackcheck = true;
             
        }

    }
}
