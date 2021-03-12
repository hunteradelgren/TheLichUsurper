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
    public bool status;
    public GameObject arm;
    public Animator animator;
    
    public float loadingTimer;

    public Camera cam;

    public GameObject HitPoint;

    float angle = 270;

    public GameObject center; //center of room the player is in

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
        if (Time.timeScale != 0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;

            Vector3 objectPos = cam.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;




            HitPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            if (angle < 0)
            {
                animator.SetFloat("PlayerRot", angle + 359);
            }
            else
            {
                animator.SetFloat("PlayerRot", angle);
            }
            setDirection();

            if (loadingTimer <= 0)
            {
                horizontalAxis = Input.GetAxis("Horizontal"); //gets the horizontal input
                verticalAxis = Input.GetAxis("Vertical"); //gets the vertical input
                if (horizontalAxis != 0 || verticalAxis != 0)
                {
                    animator.SetBool("isMoving", true);
                }
                else 
                {
                    animator.SetBool("isMoving", false);
                }
                transform.Translate(new Vector2(horizontalAxis, verticalAxis) * moveSpeed * Time.deltaTime, Space.World); //moves in direction of the input
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
        //moves the player towards the center of the room slightly when they touch a wall
        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag == "Wall")
            {
                transform.Translate((center.transform.position - transform.position) * moveSpeed / 6 * Time.deltaTime, Space.World);
            }
        }
        //updates the center based on what room the player is in
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "SpawnPoint")
            {
                center = collision.gameObject;
            }
        }
    
    public void upgrade()
    {
        status = !status;

        if (status)
        {
            moveSpeed += 5;
            print("speed upped");
        }
        else
        {
            moveSpeed -= 5;
            print("speed downed");
        }
        
    }

    //Sets direction for the animator
    public void setDirection()
    {
        //diag up right
        if (animator.GetFloat("PlayerRot") < 75 && animator.GetFloat("PlayerRot") > 5)
        {
            animator.SetInteger("Direction", 0);
        }
        //up
        else if (animator.GetFloat("PlayerRot") < 105 && animator.GetFloat("PlayerRot") > 75)
        {
            animator.SetInteger("Direction", 1);
        }
        //diag up left
        else if (animator.GetFloat("PlayerRot") < 165 && animator.GetFloat("PlayerRot") > 105)
        {
            animator.SetInteger("Direction", 2);
        }
        //left
        else if (animator.GetFloat("PlayerRot") < 195 && animator.GetFloat("PlayerRot") > 165)
        {
            animator.SetInteger("Direction", 3);
        }
        //diag down left
        else if (animator.GetFloat("PlayerRot") < 255 && animator.GetFloat("PlayerRot") > 195)
        {
            animator.SetInteger("Direction", 4);
        }
        //down
        else if (animator.GetFloat("PlayerRot") < 285 && animator.GetFloat("PlayerRot") > 255)
        {
            animator.SetInteger("Direction", 5);
        }
        //diag down right
        else if (animator.GetFloat("PlayerRot") < 359 && animator.GetFloat("PlayerRot") > 285)
        {
            animator.SetInteger("Direction", 6);
        }
        //right
        else
        {
            animator.SetInteger("Direction", 7);
        }
    }
}

