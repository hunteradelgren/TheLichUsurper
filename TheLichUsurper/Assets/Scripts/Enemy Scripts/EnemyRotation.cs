using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float rotSpeed;

    public GameObject playerTarget; //sets up the ranged target to be the player

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        target = playerTarget.transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position; //gets a vector in the direction of the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //finds angle to target location
        
        if (angle < 0)
            animator.SetFloat("EnemyRot", angle + 359);
        else
            animator.SetFloat("EnemyRot", angle);

        if(animator.GetFloat("EnemyRot") < 135 && animator.GetFloat("EnemyRot") > 45)
        {
            animator.SetInteger("Direction", 0);
        }
        else if (animator.GetFloat("EnemyRot") < 225 && animator.GetFloat("EnemyRot") > 135)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (animator.GetFloat("EnemyRot") < 315 && animator.GetFloat("EnemyRot") > 225)
        {
            animator.SetInteger("Direction", 2);
        }
        else
        {
            animator.SetInteger("Direction", 3);
        }
    }
}
