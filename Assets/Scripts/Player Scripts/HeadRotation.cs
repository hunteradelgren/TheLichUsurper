using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    [SerializeField]
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //raycast from mouse to level. Point hit is the target to look at
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500))
        {
            Vector3 direction = new Vector3(hit.point.x,hit.point.y,transform.position.z) - transform.position; //gets a vector in the direction of the target
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //finds angle to target location
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.up); //creates rotation towards target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed); //rotates to target rotation
        }
    }
}
