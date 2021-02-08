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

    private float timer = 0f;
    private bool wanderMode;
    private Vector3 wanderTarget;
    private float distance; //will hold the distance between the target and the enemy

    // Start is called before the first frame update
    void Start()
    {
        setWanderTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //new wander target can only be set every 2 seconds
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            setWanderTarget();
        }

        distance = Vector3.Distance(target.position, transform.position); //finds the distance from the target location

        setWanderMode(); 

        if (wanderMode)
        {
            moveTowardsWanderTarget();
        }
        else
        {
            moveTowardsTarget();
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
        wanderTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));
    }

    void moveTowardsTarget()
    {
        
        //checks to see if the enemy is within stopping range and returns if so
        if (distance <= targetDist)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, movSpeed * Time.deltaTime); //moves towards the target
    }

    void moveTowardsWanderTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, movSpeed * Time.deltaTime); //moves towards the wander target
    }
}
