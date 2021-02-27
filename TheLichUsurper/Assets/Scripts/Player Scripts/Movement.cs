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
    int floor;
    Rigidbody prb;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        floor = LayerMask.GetMask("Quad");
        prb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal"); //gets the horizontal input
        verticalAxis = Input.GetAxis("Vertical"); //gets the vertical input
        transform.Translate(new Vector3(horizontalAxis, 0, verticalAxis) * moveSpeed * Time.deltaTime); //moves in direction of the input
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, 100f, floor))
        {
            Vector3 mousePoint = floorHit.point - transform.position;
            mousePoint.y = 0f;
            Quaternion newRot = Quaternion.LookRotation(mousePoint);
            prb.MoveRotation(newRot);
        }
    }
}
