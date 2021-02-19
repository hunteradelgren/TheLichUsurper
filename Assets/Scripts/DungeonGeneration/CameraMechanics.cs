using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMechanics : MonoBehaviour
{
    public Room currentRoom;
    public float moveSpeed;

    private Vector3 targetPos;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        //updatePosition();
    }


    void updatePosition()
    {

        targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);





    }

    Vector3 GetCameraTargetPosition()
    {


        Vector3 targetPosition = currentRoom.transform.position;
        targetPosition.z = transform.position.z;

        
        return targetPosition;
    }
}
