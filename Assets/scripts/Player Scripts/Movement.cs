using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; //player's movement speed

    //stores key inputs
    private float horizontalAxis; 
    private float verticalAxis;
    Vector3 movin;
    Rigidbody prb;

    // Start is called before the first frame update
    void Start()
    {
        prb = GetComponent<Rigidbody>();//using a rigid body to move
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal"); //gets the horizontal input
        verticalAxis = Input.GetAxis("Vertical"); //gets the vertical input
        movin.Set(horizontalAxis, 0f, verticalAxis);//puts the vector together based on the inputs
        movin = movin.normalized.normalized * moveSpeed * Time.deltaTime;//determines the amount to move
        prb.MovePosition(transform.position + movin);//moves the player indipendently of its own direction
        //transform.Translate(new Vector3(horizontalAxis, 0, verticalAxis) * moveSpeed * Time.deltaTime); //moves in direction of the input
    }
}
