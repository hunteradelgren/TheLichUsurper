using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;

    public Room currentRoom;

    public BoxCollider2D padlock;

    private RoomTemplate template;

    // Start is called before the first frame update
    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        padlock = GetComponent<BoxCollider2D>();
        currentRoom = this.transform.parent.gameObject.GetComponent<Room>();
        isLocked = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(template.currentRoom == currentRoom)
        {
            Invoke("LockDoor",1f);
        }

       
     padlock.isTrigger = !isLocked;
    }


    private void LockDoor()
    {

        if (currentRoom.isCleared == false)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }

    }





}
