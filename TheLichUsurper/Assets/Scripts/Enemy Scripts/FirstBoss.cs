using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float movSpeed = 50f;
    public float lungeSpeed = 200f;
    public float attackDamage = 2f;
    public float lungeDamage = 2f;
    public float attackRate = 2f;
    public float chargeTime = 3f;
    public float lungeTime = 1f;
    public float attackRange = 5f;
    public float maxLungeTime = 2f;

    public bool isAttacking = false;
    public bool isLunging = false;
    private float distPlayer;
    private float distPos1;
    private float distPos2;
    private float distPos3;
    private float rotSpeed = 180;
    private bool inRange = true; //is target in range
    private bool canAttack = true; //is attack on cooldown
    private float cooldownTimer = 0f; //timer for cooldown
    private float chargeTimer = 0f; //timer for attack chargeup
    private float lungeTimer = 0f;
    private bool validTarget = false; //does the target have player health
    private List<GameObject> colliding = new List<GameObject>();
    private Vector2 lungeTarget;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerHealth>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //finds the distance from the target location
            distPlayer = Vector2.Distance(Player.position, transform.position); 
            //distPos1 = Vector2.Distance(pos1.position, transform.position); 
           // distPos2 = Vector2.Distance(pos2.position, transform.position); 
            //distPos3 = Vector2.Distance(pos3.position, transform.position);
            
            //boss is lunging
            if (isLunging)
            {
                Vector2 direction = lungeTarget - new Vector2(transform.position.x, transform.position.y); //gets a vector in the direction of the target
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
                Quaternion targetRot = Quaternion.AngleAxis(angle-90, Vector3.back); //creates rotation towards target
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation

                //charging up the lunge
                chargeTimer += Time.deltaTime;
                if(chargeTimer >= lungeTime)
                {
                    //lunge is being performed
                    lungeTimer += Time.deltaTime;

                    rb.MovePosition(((new Vector2(transform.position.x,transform.position.y)+(lungeTarget-new Vector2(transform.position.x,transform.position.y)).normalized * lungeSpeed*Time.deltaTime)));
                    if(lungeTimer >= maxLungeTime)
                    {
                        //lunge is finished
                        chargeTimer = 0;
                        lungeTimer = 0;
                        isLunging = false;
                        canAttack = false;
                    }
                }
            }
            //boss is doing swing attack
            else if (isAttacking)
            {
                Vector2 direction = Player.position - transform.position; //gets a vector in the direction of the target
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
                Quaternion targetRot = Quaternion.AngleAxis(angle - 90, Vector3.back); //creates rotation towards target
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation

                chargeTimer += Time.deltaTime;
                if (chargeTimer >= lungeTime)
                {
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
                if (distPos1 <= 1.5 || distPos2 <= 1.5 || distPos3 <= 1.5)
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
                    rb.MovePosition(((transform.position + (pos1.position - transform.position).normalized * movSpeed * Time.deltaTime)));

                    Vector2 direction = pos1.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
                    Quaternion targetRot = Quaternion.AngleAxis(angle - 90, Vector3.back); //creates rotation towards target
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation
                }
                //position 2 is closest
                else if(distPos2 < distPos1 && distPos2 < distPos3)
                {
                    rb.MovePosition(((transform.position + (pos2.position - transform.position).normalized * movSpeed * Time.deltaTime)));

                    Vector2 direction = pos2.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
                    Quaternion targetRot = Quaternion.AngleAxis(angle - 90, Vector3.back); //creates rotation towards target
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation
                }
                //position 3 is closest
                else
                {
                    rb.MovePosition(((transform.position + (pos3.position - transform.position).normalized * movSpeed * Time.deltaTime)));

                    Vector2 direction = pos3.position - transform.position; //gets a vector in the direction of the target
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
                    Quaternion targetRot = Quaternion.AngleAxis(angle - 90, Vector3.back); //creates rotation towards target
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation
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
        if(colliding.Count == 1)
        {
            Player.GetComponent<PlayerHealth>().takeDamage(attackDamage);
            print("hit player with swing");
        }
    }

    void lungeAttack()
    {
        if(colliding.Count == 0)
        {
            lungeTarget = (Player.position - transform.position)*1.2f;
            isLunging = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLunging)
        {
            if (collision.GetComponentInParent<PlayerHealth>() != null)
            {
                Player.GetComponent<PlayerHealth>().takeDamage(lungeDamage);
                print("Lunged into player");
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            colliding.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding.Remove(collision.gameObject);
        }
    }
}
