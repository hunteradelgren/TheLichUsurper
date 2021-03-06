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
    private Rigidbody prb;
    
    public Animator animator;
    
    public float loadingTimer;

    public Camera cam;

    public GameObject HitPoint;

    float angle = 270;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = cam.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        


        HitPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
        if (angle < 0)
            animator.SetFloat("PlayerRot", angle + 359);
        else
            animator.SetFloat("PlayerRot", angle);

        if (loadingTimer <= 0)
        {
            horizontalAxis = Input.GetAxis("Horizontal"); //gets the horizontal input
            verticalAxis = Input.GetAxis("Vertical"); //gets the vertical input
            transform.Translate(new Vector2(horizontalAxis, verticalAxis) * moveSpeed * Time.deltaTime,Space.World); //moves in direction of the input
            /*Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit floorHit;
            Plane groundPlane = new Plane(Vector3.right, Vector3.zero);
            float raylength;
            if (groundPlane.Raycast(camRay, out raylength))
            {
                Vector3 pointToLook = camRay.GetPoint(raylength);
                Debug.DrawLine(camRay.origin, pointToLook, Color.blue);
                transform.LookAt(pointToLook);




                /*Vector3 mousePoint = floorHit.point - transform.position;
                mousePoint.y = 0f;
                Quaternion newRot = Quaternion.LookRotation(mousePoint);
                prb.MoveRotation(newRot);
            }*/
        }
        else
            loadingTimer -= Time.deltaTime;
    }
}

