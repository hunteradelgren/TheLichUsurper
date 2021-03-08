using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
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
    //damage each attack deals
    [SerializeField]
    float damage;

    //enemy's movement component
    private EnemyMovement enemyMove;

    private bool isAttacking = false; //the attack is being done
    private float distance; //distance to target
    private bool inRange = true; //is target in range
    private bool canAttack = true; //is attack on cooldown
    private float cooldownTimer = 0f; //timer for cooldown
    private float chargeTimer = 0f; //timer for attack chargeup
    private bool validTarget = false; //does the target have player health

    public GameObject playerTarget; //sets up the ranged target to be the player

    // Start is called before the first frame update
    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        target = playerTarget;
    }

    // Update is called once per frame
    void Update()
    {
            //enemy movement boolean is synchronized to the attack
            enemyMove.isAttacking = isAttacking;
            checkCanAttack();
            checkInRange();
            checkValidTarget();

            //enemy is not already attacking
            if (!isAttacking)
            {
                if (canAttack && inRange && validTarget)
                {
                    //enemy is now in the process of atttacking
                    isAttacking = true;
                }
            }

            //enemy is in the process of attacking
            if (isAttacking)
            {
                chargeTimer += Time.deltaTime;

                if (chargeTimer >= chargeTime)
                {
                    isAttacking = false;
                    canAttack = false;
                    chargeTimer = 0f;
                    Attack();                    
                }
            }
    }

    void checkValidTarget()
    {
        //does the target have a player health component
        if (target.GetComponent<PlayerHealth>() != null)
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

    void Attack()
    {
        checkInRange();
        checkValidTarget();
        if(validTarget && inRange)
        {
            print("Attacked" + Time.time);
            target.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}
