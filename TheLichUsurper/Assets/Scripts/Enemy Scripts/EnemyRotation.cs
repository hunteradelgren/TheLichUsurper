using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position; //gets a vector in the direction of the target
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //finds angle to target location
        Quaternion targetRot = Quaternion.AngleAxis(angle-90, Vector3.up); //creates rotation towards target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation
    }
}
