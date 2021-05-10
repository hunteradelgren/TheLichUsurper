using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    public playerStatsManager stats;

    public Transform Player;

    [SerializeField]
    string portalName1;
    [SerializeField]
    string portalName2;
    [SerializeField]
    string portalName3;
    [SerializeField]
    string portalName4;
    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject portal4;

    public float Maxhealth = 200f;
    public float currentHealth;
    public float moveSpeed;

    public Vector2 velocity;
    public bool wasHit = false;
    public float hitTimer = 0f;

    public int numPortals = 2;

    public float attackRate = 1f;
    public float swingDist = 2f;
    public float targetChaseDist = 1f;
    public float swingDamage = 10f;
    public float rangeDamage = 10f;
    public bool isAttacking = false;
    public bool inRange;
    public bool canAttack;
    public bool validTarget;
    public float Timer = 0f;
    public bool spawnedPortals = false;

    public float vulnerableTimer = 0f;
    public bool isVulnerable = false;

    private List<GameObject> colliding = new List<GameObject>();

    private RoomTemplate template;
    public Room spawnRoom;

    public GameObject center; //uses the center game object in the room
    public Animator animator;
    public SpriteRenderer bossSprite;

    public float stanceTimer = 0f;
    public bool meleeStance = true;

    private Vector2 wanderTarget;

    public Vector2 leftPortal;
    public Vector2 rightPortal;
    public Vector2 upPortal;
    public Vector2 downPortal;

    public AudioClip laughClip;
    public AudioClip swingClipHit;
    public AudioClip swingClipMiss;
    public AudioClip hitClip;
    public AudioClip poof;
    public AudioSource sound;

    public bool hasDied = false;

    //name of the projectile in the resources folder
    [SerializeField]
    string projectileName;
    private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        projectilePrefab = Resources.Load<GameObject>(projectileName);
        currentHealth = Maxhealth;
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        animator = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();

        portal1 = Resources.Load<GameObject>(portalName1);
        portal2 = Resources.Load<GameObject>(portalName2);
        portal3 = Resources.Load<GameObject>(portalName3);
        portal4 = Resources.Load<GameObject>(portalName4);

        leftPortal = new Vector2(center.transform.position.x + 2, center.transform.position.y);
        rightPortal = new Vector2(center.transform.position.x - 2, center.transform.position.y);
        upPortal = new Vector2(center.transform.position.x, center.transform.position.y + 2);
        downPortal = new Vector2(center.transform.position.x, center.transform.position.y - 2);

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //is boss dead
        if (currentHealth <= 0)
        {
            if (!hasDied)
            {
                animator.SetTrigger("Died");
                hasDied = true;
            }
            else
            {
                return;
            }
        }
        //boss sprite is colored red if hit
        if (wasHit)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= .15)
            {
                bossSprite.color = new Color(1, 1, 1);
                wasHit = false;
                hitTimer = 0;
            }
        }

        Vector2 direction = Player.position - transform.position; //gets a vector in the direction of the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

        if (angle < 0)
            animator.SetFloat("EnemyRot", angle + 359);
        else
            animator.SetFloat("EnemyRot", angle);
        setDirection();

        /////////////////////////////////////////////////////////////
        ///Boss is Active
        /////////////////////////////////////////////////////////////
        if (spawnRoom == template.currentRoom)
        {
            //boss switches stances every 10 seconds
            stanceTimer += Time.deltaTime;
            if (stanceTimer >= 10)
            {
                print("stance switch");
                meleeStance = !meleeStance;
                stanceTimer = 0;
            }
            if (spawnRoom.enemyCount > 1)
            {
                isVulnerable = false;
                animator.SetBool("isVulnerable", false);
            }
            if (spawnRoom.enemyCount == 1 && spawnedPortals && !isVulnerable)
            {
                vulnerableTimer += Time.deltaTime;
                if (vulnerableTimer > 10)
                {
                    
                    isVulnerable = true;
                    vulnerableTimer = 0;
                    print("isvulnerable");
                    animator.SetBool("isVulnerable", true);
                }
                print("vulnerable buffer not reached");
            }



            //boss is in vulnerable stance
            //stays still and is damageable. summons pillars to spawn enemies when leaving this state
            if (isVulnerable)
            {
                spawnedPortals = false;
                vulnerableTimer += Time.deltaTime;
                if(vulnerableTimer >= 10)
                {
                    animator.SetBool("isVulnerable", false);
                    print("is not vulnerable");
                    vulnerableTimer = 0;
                    isVulnerable = false;
                }
            }

            else if (!spawnedPortals)
            {
                sound.PlayOneShot(laughClip);
                print("spawn portals");
                animator.SetTrigger("SpawnPortals");
                spawnedPortals = true;
            }

            //boss is in melee stance
            //chases player swinging sword
            else if (meleeStance)
            {
                print("melee stance");
                    moveSpeed = 5f; //melee stance movement spped
                    checkCanAttack();
                    checkInRange();
                    ///boss is doing swing attack
                    if (isAttacking)
                    {
                        direction = Player.position - transform.position; //gets a vector in the direction of the target
                        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

                        if (angle < 0)
                            animator.SetFloat("EnemyRot", angle + 359);
                        else
                            animator.SetFloat("EnemyRot", angle);
                        setDirection();
                        animator.SetBool("isAttacking", true);
                    }
                    //boss is chasing player until reaching the target distance
                    else if (Vector2.Distance(Player.transform.position, transform.position) >= targetChaseDist)
                    {
                        velocity = Player.position - transform.position;
                        transform.Translate(velocity.normalized * moveSpeed * Time.deltaTime, Space.World);
                        direction = Player.position - transform.position; //gets a vector in the direction of the target
                        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location

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

            //boss is in range stance
            //wanders around shooting
            else
            {
                print("Range stance");
                moveSpeed = 2f; //ranged stance move speed
                if (Vector2.Distance(wanderTarget, transform.position) < 1 || wanderTarget == null)
                {
                    wanderTarget = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
                }
                velocity = new Vector2(wanderTarget.x - transform.position.x, wanderTarget.y - transform.position.y);
                velocity = velocity.normalized;
                checkCanAttack();
                if (canAttack)
                {
                    animator.SetTrigger("Shoot");
                    canAttack = false;
                }
                
            }
        }
        /////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////
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
            Timer += Time.deltaTime; //adds to the timer
            //cooldown time is finished
            if (Timer >= attackRate)
            {
                //allow enemy to attack
                canAttack = true;
                Timer = 0f;
            }
            animator.SetBool("isAttacking", false);
        }
    }

    void checkInRange()
    {
        if (Vector2.Distance(Player.transform.position, transform.position) <= swingDist) //target is in range
        {
            inRange = true;
        }
        else //target is out of range
        {
            inRange = false;
        }
    }

    public void swingAttack()
    {
        
        //player is in range for swing attack
        if (Vector2.Distance(Player.transform.position,transform.position)<=swingDist)
        {
            sound.PlayOneShot(swingClipHit);
            Player.GetComponent<PlayerHealth>().takeDamage(swingDamage);
            print("hit player with swing");
        }
        else
        {
            sound.PlayOneShot(swingClipMiss);
        }
        animator.SetBool("isAttacking", false);
        isAttacking = false;
        canAttack = false;
        Timer = 0f;
    }

    void ShootProjectile()
    {
        //instantiates the projectile
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        //moves the projectile on top of the enemy
        projectile.transform.position = transform.position;
        //rotates projectile to where the enemy is facing
        projectile.transform.rotation = Quaternion.Euler(0, 0, animator.GetFloat("EnemyRot"));

        projectile.GetComponent<basicProjectileBehavior>().damage = rangeDamage;
        animator.SetBool("isAttacking", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding.Add(collision.gameObject);
        }
        if (collision.tag == "Boss")
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
                wanderTarget = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
                Vector2 obstaclePos = c.transform.position;
                float dist = Vector2.Distance(obstaclePos, transform.position);
                //velocity  = current velocity + ((position - obstacle position) + Random(-15,15)) * distance/100)
                velocity = new Vector2(velocity.x + (center.transform.position.x - transform.position.x), velocity.y + (center.transform.position.y - transform.position.y)).normalized;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isVulnerable)
        {
            sound.PlayOneShot(hitClip);
            currentHealth -= amount;
            bossSprite.color = new Color(1, 0, 0);
            wasHit = true;
        }
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

    public void spawnPortals()
    {
        GameObject portal = Instantiate<GameObject>(portal1, transform.parent);
        portal.transform.position = leftPortal;
        portal.GetComponent<SpawnFromPortal>().spawnroom = spawnRoom;

        portal = Instantiate<GameObject>(portal2, transform.parent);
        portal.transform.position = rightPortal;
        portal.GetComponent<SpawnFromPortal>().spawnroom = spawnRoom;

        portal = Instantiate<GameObject>(portal3, transform.parent);
        portal.transform.position = upPortal;
        portal.GetComponent<SpawnFromPortal>().spawnroom = spawnRoom;

        portal = Instantiate<GameObject>(portal4, transform.parent);
        portal.transform.position = downPortal;
        portal.GetComponent<SpawnFromPortal>().spawnroom = spawnRoom;
    }

    public void playPoof()
    {
        sound.PlayOneShot(poof);
    }
    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(4);
    }
}
