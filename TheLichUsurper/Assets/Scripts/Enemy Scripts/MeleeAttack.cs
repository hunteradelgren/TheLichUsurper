using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
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

    private bool isAttacking = false; //the attacj is being done
    private float distance; //distance to target
    private bool inRange = true; //is target in range
    private bool canAttack = true; //is attack on cooldown
    private float cooldownTimer = 0f; //timer for cooldown
    private float chargeTimer = 0f; //timer for attack chargeup

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            }
        }

        //enemy is in the process of attacking
        if (isAttacking)
        {
            chargeTimer += Time.deltaTime;

            if(chargeTimer >= chargeTime)
            {
                //message will be replaced with actual attack later when player health is made
                print("Attack has been made");
                chargeTimer = 0f;
                isAttacking = false;
                canAttack = false;
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
}
