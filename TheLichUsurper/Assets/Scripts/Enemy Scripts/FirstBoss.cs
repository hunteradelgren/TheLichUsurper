using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstBoss : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField]
    Transform Player;

    [SerializeField]
    Transform pos1;

    [SerializeField]
    Transform pos2;

    [SerializeField]
    Transform pos3;

    public float movSpeed = 10f;
    public float lungeSpeed = 50f;
    public float attackDamage = 1f;
    public float lungeDamage = .5f;
    public float attackRate = 2f;
    public float chargeTime = 3f;
    public float lungeTime = 1f;
    public float attackRange = 1f;
    public float maxLungeTime = 2f;
    public float maxhealth = 50f;
    public float targetChaseDist = 2f;
    public float currentHealth = 50f;
    

    public bool isAttacking = false;
    public bool isLunging = false;

    private float distPlayer;
    private float distPos1;
    private float distPos2;
    private float distPos3;

    private float maxMoveSpeed;
    private float maxLungeSpeed;
    private float rotSpeed = 360;
    private bool inRange = true; //is target in range
    private bool canAttack = true; //is attack on cooldown
    private float cooldownTimer = 0f; //timer for cooldown
    private float chargeTimer = 0f; //timer for attack chargeup
    private float lungeTimer = 0f;
    private bool validTarget = false; //does the target have player health
    private List<GameObject> colliding = new List<GameObject>();
    private Vector2 lungeTarget;
    private Rigidbody2D rb;
    private Vector2 velocity;
    public GameObject center; //uses the center game object in the room
    public Animator animator;

    private RoomTemplate template;
    public Room spawnRoom;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxhealth;
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Object.Destroy(gameObject);
            SceneManager.LoadScene(3);

        }
            //finds the distance from the target location
            distPlayer = Vector2.Distance(Player.position, transform.position); 
            distPos1 = Vector2.Distance(pos1.position, transform.position); 
            distPos2 = Vector2.Distance(pos2.position, transform.position); 
            distPos3 = Vector2.Distance(pos3.position, transform.position);
        if (spawnRoom == template.currentRoom)
        {

            //boss is over 30% health and so will use 1st phase of behavior
            if (currentHealth >= maxhealth * .3)
            {

                //boss is lunging
                if (isLunging)
                {
                    Vector2 direction = lungeTarget; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                    if (angle < 0)
                        animator.SetFloat("EnemyRot", angle + 359);
                    else
                        animator.SetFloat("EnemyRot", angle);
                    setDirection();

                    //charging up the lunge
                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= lungeTime)
                    {
                        animator.SetBool("isMoving", true);
                        //lunge is being performed
                        lungeTimer += Time.deltaTime;
                        velocity = lungeTarget;
                        transform.Translate(velocity.normalized * lungeSpeed * Time.deltaTime, Space.World);
                        if (lungeTimer >= maxLungeTime)
                        {
                            //lunge is finished
                            chargeTimer = 0;
                            lungeTimer = 0;
                            isLunging = false;
                            canAttack = false;
                            animator.SetBool("isMoving", false);
                        }
                    }
                }
                //boss is doing swing attack
                else if (isAttacking)
                {
                    Vector2 direction = Player.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                    if (angle < 0)
                        animator.SetFloat("EnemyRot", angle + 359);
                    else
                        animator.SetFloat("EnemyRot", angle);
                    setDirection();

                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= lungeTime)
                    {
                        animator.SetBool("isAttacking", true);
                        swingAttack();
                        chargeTimer = 0;
                        isAttacking = false;
                        canAttack = false;
                    }
                }
                //boss is not currently attacking and will move towards an attack position
                else
                {
                    checkCanAttack();

                    //boss is at an attack position
                    if (distPos1 <= .5 || distPos2 <= .5 || distPos3 <= .5)
                    {
                        if (canAttack)
                        {
                            checkInRange();
                            checkValidTarget();
                            if (inRange && validTarget)
                            {
                                isAttacking = true;
                            }
                            else if (!inRange && validTarget)
                            {
                                lungeAttack();
                            }
                        }
                    }

                    //position 1 is closest
                    else if (distPos1 < distPos2 && distPos1 < distPos3)
                    {
                        velocity = pos1.position - transform.position;
                        transform.Translate(velocity.normalized * movSpeed * Time.deltaTime, Space.World);
                        Vector2 direction = pos1.position - transform.position; //gets a vector in the direction of the target
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                        if (angle < 0)
                            animator.SetFloat("EnemyRot", angle + 359);
                        else
                            animator.SetFloat("EnemyRot", angle);
                        setDirection();
                    }
                    //position 2 is closest
                    else if (distPos2 < distPos1 && distPos2 < distPos3)
                    {
                        velocity = pos2.position - transform.position;
                        transform.Translate(velocity.normalized * movSpeed * Time.deltaTime, Space.World);
                        Vector2 direction = pos2.position - transform.position; //gets a vector in the direction of the target
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                        if (angle < 0)
                            animator.SetFloat("EnemyRot", angle + 359);
                        else
                            animator.SetFloat("EnemyRot", angle);
                        setDirection();
                    }
                    //position 3 is closest
                    else
                    {
                        velocity = pos3.position - transform.position;
                        transform.Translate(velocity.normalized * movSpeed * Time.deltaTime, Space.World);
                        Vector2 direction = pos3.position - transform.position; //gets a vector in the direction of the target
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                        if (angle < 0)
                            animator.SetFloat("EnemyRot", angle + 359);
                        else
                            animator.SetFloat("EnemyRot", angle);
                        setDirection();
                    }
                }
            }

            //boss is below health threshold and is using second phase of behavior
            else
            {
                checkCanAttack();
                checkInRange();
                ///boss is doing swing attack
                if (isAttacking)
                {
                    Vector2 direction = Player.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                    if (angle < 0)
                        animator.SetFloat("EnemyRot", angle + 359);
                    else
                        animator.SetFloat("EnemyRot", angle);
                    setDirection();

                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= lungeTime)
                    {
                        print("swinging");
                        swingAttack();
                        chargeTimer = 0;
                        isAttacking = false;
                        canAttack = false;
                    }
                }
                //boss is chasing player until reaching the target distance
                else if (distPlayer >= targetChaseDist)
                {
                    velocity = Player.position - transform.position;
                    transform.Translate(velocity.normalized * movSpeed *Time.deltaTime, Space.World);

                    Vector2 direction = Player.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                    if (angle < 0)
                        animator.SetFloat("EnemyRot", angle + 359);
                    else
                        animator.SetFloat("EnemyRot", angle);
                    setDirection();
                }
                else
                {
                    //player is inrange
                    if (canAttack && inRange)
                    {
                        isAttacking = true;
                    }
                }
            }
        }
    }

    void checkValidTarget()
    {
        //does the target have a player health component
        if (Player.GetComponent<PlayerHealth>() != null)
        {
            validTarget = true;
        }
        else
        {
            validTarget = false;
        }
    }

    void checkCanAttack()
    {
        //enemy's attack is on cooldown
        if (!canAttack)
        {
            cooldownTimer += Time.deltaTime; //adds to the timer
            //cooldown time is finished
            if (cooldownTimer >= attackRate)
            {
                //allow enemy to attack
                canAttack = true;
                cooldownTimer = 0f;
            }
        }
    }

    void checkInRange()
    {
        //gets the distance to the target
        distPlayer = Vector2.Distance(Player.transform.position, transform.position);
        if (distPlayer <= attackRange) //target is in range
        {
            inRange = true;
        }
        else //target is out of range
        {
            inRange = false;
        }
    }

    void swingAttack()
    {
        //player is colliding with capsule collider for swing attack
        if(colliding.Count != 0)
        {
            Player.GetComponent<PlayerHealth>().takeDamage(attackDamage);
            print("hit player with swing");
            animator.SetBool("isAttacking", false);
        }
    }

    void lungeAttack()
    {
        if(colliding.Count == 0)
        {
            lungeTarget = (Player.position - transform.position)*1f;
            isLunging = true;
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLunging && lungeTimer != 0)
        {
            if (collision.GetComponentInParent<PlayerHealth>() != null)
            {
                Player.GetComponent<PlayerHealth>().takeDamage(lungeDamage);
                print("Lunged into player");
            }
            if(collision.tag == "Wall" || collision.GetComponent<Door>() != null)
            {
                //lunge is finished
                chargeTimer = 0;
                lungeTimer = 0;
                isLunging = false;
                canAttack = false;
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            print(collision.name);
            colliding.Add(collision.gameObject);
        }


        if (collision.tag == "EndRoom")
        {
            spawnRoom = collision.GetComponent<Room>();
        }
            
        if (collision.tag == "Wall" || collision.GetComponent<Door>() != null)
        {
            avoidWall();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding.Remove(collision.gameObject);
        }
    }

    void avoidWall()
    {
        foreach (GameObject c in colliding)
        {
            if (c.tag == "Wall" || c.GetComponent<Door>() != null)
            {
                Vector2 obstaclePos = c.transform.position;
                float dist = Vector2.Distance(obstaclePos, transform.position);
                //velocity  = current velocity + ((position - obstacle position) + Random(-15,15)) * distance/100)
                velocity = new Vector2(velocity.x + (center.transform.position.x - transform.position.x), velocity.y + (center.transform.position.y - transform.position.y)).normalized;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void setDirection()
    {
        //diag up right
        if (animator.GetFloat("EnemyRot") < 75 && animator.GetFloat("EnemyRot") > 5)
        {
            animator.SetInteger("Direction", 0);
        }
        //up
        else if (animator.GetFloat("EnemyRot") < 105 && animator.GetFloat("EnemyRot") > 75)
        {
            animator.SetInteger("Direction", 1);
        }
        //diag up left
        else if (animator.GetFloat("EnemyRot") < 165 && animator.GetFloat("EnemyRot") > 105)
        {
            animator.SetInteger("Direction", 2);
        }
        //left
        else if (animator.GetFloat("EnemyRot") < 195 && animator.GetFloat("EnemyRot") > 165)
        {
            animator.SetInteger("Direction", 3);
        }
        //diag down left
        else if (animator.GetFloat("EnemyRot") < 255 && animator.GetFloat("EnemyRot") > 195)
        {
            animator.SetInteger("Direction", 4);
        }
        //down
        else if (animator.GetFloat("EnemyRot") < 285 && animator.GetFloat("EnemyRot") > 255)
        {
            animator.SetInteger("Direction", 5);
        }
        //diag down right
        else if (animator.GetFloat("EnemyRot") < 359 && animator.GetFloat("EnemyRot") > 285)
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
