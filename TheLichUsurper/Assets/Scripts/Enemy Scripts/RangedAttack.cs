using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public bool isActive = false;

    //target object
    [SerializeField]
    GameObject target;
    //distance that the enemy can attack from
    [SerializeField]
    float attackRange;
    //time it takes to complete an attack
    [SerializeField]
    float chargeTime;
    //time between attacks
    [SerializeField]
    float attackRate;
    //name of the projectile in the resources folder
    [SerializeField]
    string projectileName;
    public float damage;

    private bool isAttacking = false; //the attacj is being done
    private float distance; //distance to target
    private bool inRange = true; //is target in range
    private bool canAttack = true; //is attack on cooldown
    private float cooldownTimer = 0f; //timer for cooldown
    private float chargeTimer = 0f; //timer for attack chargeup
    private GameObject projectilePrefab;

    public GameObject playerTarget; //sets up the ranged target to be the player
    public Room spawnRoom;
    private RoomTemplate template;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //saves prefab of serialized name in the resources folder as a game object
        projectilePrefab = Resources.Load<GameObject>(projectileName);
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        target = playerTarget;
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRoom == template.currentRoom)
        {
            checkCanAttack();
            checkInRange();

            //enemy is not already attacking
            if (!isAttacking)
            {
                if (canAttack && inRange)
                {
                    //enemy is now in the process of atttacking
                    isAttacking = true;
                    animator.SetBool("isAttacking", true);
                }
            }

            //enemy is in the process of attacking
            if (isAttacking)
            {
                chargeTimer += Time.deltaTime;
                //attack has finished charging
                if (chargeTimer >= chargeTime)
                {
                    ShootProjectile();
                    //resets values
                    chargeTimer = 0f;
                    isAttacking = false;
                    canAttack = false;
                }
            }


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
        distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance <= attackRange) //target is in range
        {
            inRange = true;
        }
        else //target is out of range
        {
            inRange = false;
        }
    }

    void ShootProjectile()
    {
        //instantiates the projectile
        GameObject projectile =  Instantiate<GameObject>(projectilePrefab);
        //moves the projectile on top of the enemy
        projectile.transform.position = transform.position;
        //rotates projectile to where the enemy is facing
        projectile.transform.rotation = Quaternion.Euler(0, 0, animator.GetFloat("EnemyRot"));

        projectile.GetComponent<basicProjectileBehavior>().damage = damage;
        animator.SetBool("isAttacking", false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room" || other.tag == "EndRoom" || other.tag == "BottomRoom")
            spawnRoom = other.GetComponent<Room>();
    }
}

