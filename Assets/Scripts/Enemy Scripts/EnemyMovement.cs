using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField]
    Transform target; //the target object's Transform

    public float targetDist; //how close to the target will the enemy try to get

    [SerializeField]
    float movSpeed; //movement speed

    [SerializeField]
    float chaseDist; //how close does the target have to be for the ai to chase. 

    [SerializeField]
    float runDist; //how close to the target before entering wander mode to get away.

    private List<GameObject> colliding = new List<GameObject>();

    private float timer = 0f;
    public bool wanderMode;
    private Vector2 wanderTarget;
    public float distance; //will hold the distance between the target and the enemy
    private Rigidbody2D rb;
    private Vector2 velocity;
    private RoomTemplate template;
    private bool hitObstacle = false;
    private float obsTimer = 0f;


    //enemy will not move while it swings
    public bool isAttacking = false;

    public Room spawnRoom;

    public GameObject playerTarget; //sets up the target to be the player
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {   
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        target = playerTarget.transform;
        setWanderTarget();
        rb = GetComponent<Rigidbody2D>();
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRoom == template.currentRoom)
        {
            //if enemy is not attacking
            if (!isAttacking)
            {
                animator.SetBool("isMoving", true);
                //new wander target can only be set every 2 seconds
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    setWanderTarget();
                }
                if (hitObstacle)
                {
                    obsTimer += Time.deltaTime;
                    wanderMode = true;
                    if(obsTimer >= .5)
                    {
                        wanderMode = false;
                        hitObstacle = false;
                        obsTimer = 0f;
                    }
                }
                distance = Vector2.Distance(target.position, transform.position); //finds the distance from the target location

                setWanderMode();

                //set velocity towards target or wandertarget
                if (wanderMode)
                {
                    velocityTowardsWanderTarget();
                }
                else
                {
                    velocityTowardsTarget();
                }

                //is enemy colliding with obstacle
                if (colliding.Count > 0)
                {
                    avoidObstacle();
                    avoidWall();
                }
                    

                velocity *= movSpeed * Time.deltaTime;
                if (distance >= targetDist || wanderMode)
                {
                    transform.Translate(velocity,Space.World);
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void setWanderMode()
    {
        if (target == null || hitObstacle)
        {
            //no target exists. Stay in wander mode
            wanderMode = true;
        }
        else if (distance <= chaseDist && distance >= runDist) 
        {
            //target is within the preferred range
            wanderMode = false;
        }
        else
        {
            //there is a target but the target is outside the preferred range
            wanderMode = true;
        }
    }

    void setWanderTarget()
    {
        //resets timer
        timer = 0f;
        //sets the wander target to a random position within a range of -10 to 10 in both directions
        wanderTarget = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10));
    }

    void velocityTowardsTarget()
    {
        
        velocity = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        velocity = velocity.normalized;
    }

    void velocityTowardsWanderTarget()
    {
        velocity = new Vector2(wanderTarget.x - transform.position.x, wanderTarget.y - transform.position.y);
        velocity = velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Wall" || collision.gameObject.GetComponent<Door>() != null)
        {
            colliding.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Wall" || collision.gameObject.GetComponent<Door>() != null)
        {
            colliding.Remove(collision.gameObject);
        }
    }

    void avoidObstacle()
    {
        foreach(GameObject c in colliding)
        {
            if (c.tag == "Obstacle")
            {
                hitObstacle = true;
                setWanderTarget();
                Vector2 obstaclePos = c.transform.position;
                float dist = Vector2.Distance(obstaclePos, transform.position);
                //velocity  = current velocity + ((position - obstacle position) + Random(-15,15)) * distance/100)
                velocity = new Vector2(velocity.x + (((transform.position.x - c.transform.position.x) + Random.Range(-15, 15)) * (100)), velocity.y + (((transform.position.y - c.transform.position.y) + Random.Range(-15, 15)) * (100))).normalized;
                
            }
        }
    }

    void avoidWall()
    {
        foreach (GameObject c in colliding)
        {
            print(c.gameObject.name);
            if (c.tag == "Wall" || c.GetComponent<Door>() != null)
            {
                Vector2 obstaclePos = c.transform.position;
                float dist = Vector2.Distance(obstaclePos, transform.position);
                //velocity  = current velocity + ((position - obstacle position) + Random(-15,15)) * distance/100)
                velocity = new Vector2(velocity.x + (spawnRoom.transform.position.x-transform.position.x),velocity.y+(spawnRoom.transform.position.y-transform.position.y)).normalized;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room" || other.tag == "EndRoom" || other.tag == "BottomRoom")
            spawnRoom = other.GetComponent<Room>();

        else if (other.tag == "Wall" || other.GetComponent<Door>() != null)
        {
            colliding.Add(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Wall" || collision.GetComponent<Door>() != null)
        {
            colliding.Remove(collision.gameObject);
        }
    }
}
