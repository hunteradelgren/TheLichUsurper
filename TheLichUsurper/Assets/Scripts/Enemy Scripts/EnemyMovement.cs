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

    private float distance; //will hold the distance between the target and the enemy

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //checks for whether there is a target
        if (target == null)
            return;

        distance = Vector2.Distance(target.position, transform.position); //finds the distance from the target location
        //checks to see if the enemy is within stopping range and returns if so
        if (distance <= targetDist)
            return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, movSpeed * Time.deltaTime); //moves towards the target
        
    }
}
