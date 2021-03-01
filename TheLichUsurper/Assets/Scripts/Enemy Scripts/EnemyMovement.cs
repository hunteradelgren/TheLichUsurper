using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform target; //the target object's Transform

    [SerializeField]
    float targetDist; //how close to the target will the enemy try to get

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
    private float Speed;


    private Room SpawnRoom; //is going to set the room it spawns in as the spawnRoom in order to control movement later
    private RoomTemplate template; //access the dungeon controller template

    //enemy will not move while it swings
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        setWanderTarget();
        rb = GetComponent<Rigidbody2D>();
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();

        Speed = movSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnRoom == template.currentRoom) //checks to see if player is in current room. if false enemies cant move
            movSpeed = Speed;
        else
        {
            movSpeed = 0;
            transform.rotation = Quaternion.Euler(0, 30, 0);
        }
            


        //if enemy is not attacking
        if (!isAttacking)
        {
            //new wander target can only be set every 2 seconds
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                setWanderTarget();
            }

            distance = Vector2.Distance(target.position, transform.position); //finds the distance from the target location

            setWanderMode();

            if (wanderMode)
            {
                velocityTowardsWanderTarget();
            }
            else
            {
                velocityTowardsTarget();
            }

            if(colliding.Count > 0)
            avoidObstacle();

            velocity *= movSpeed * Time.deltaTime;
            if(distance >= targetDist || wanderMode)
            {
                print(transform.name + "moved");
                rb.MovePosition(new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y));
            }
            
            else
            {
                rb.MovePosition(new Vector2(transform.position.x,transform.position.y));
                return;
            }
        }
    }

    void setWanderMode()
    {
        if (target == null)
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
        print(transform.name+ "pre " + velocity);
        velocity = velocity.normalized;
        print(transform.name + "post "+velocity);
    }

    void velocityTowardsWanderTarget()
    {
        velocity = new Vector2(wanderTarget.x - transform.position.x, wanderTarget.y - transform.position.y);
        velocity = velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            colliding.Add(collision.gameObject);
            print(collision.transform.name);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            colliding.Remove(collision.gameObject);
            print(collision.transform.name);
        }
    }

    void avoidObstacle()
    {
        foreach(GameObject c in colliding)
        {
            Vector2 obstaclePos = c.transform.position;
            float dist = Vector2.Distance(obstaclePos, transform.position);
            print(dist);
            velocity = new Vector2(velocity.x + (((transform.position.x - c.transform.position.x)+Random.Range(-15,15))*dist/100), velocity.y + (((transform.position.y - c.transform.position.y)+Random.Range(-15,15))*dist/100)).normalized;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room")
            SpawnRoom = other.GetComponent<Room>();


    }





}
