using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public CharacterController controller;
    public float playerspeed =4f;
    float xdirection;
    float zdirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the character.
        xdirection = Input.GetAxis("Horizontal");
        zdirection = Input.GetAxis("Vertical");
        Vector3 playerMovement = transform.right * xdirection + transform.forward * zdirection;
        controller.Move(playerMovement * playerspeed * Time.deltaTime);

    }
}
