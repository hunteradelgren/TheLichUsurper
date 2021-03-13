using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;

    public Room currentRoom;

    public BoxCollider2D padlock;

    private RoomTemplate template;

    public Animator animator;

    public bool isclear;

    

    // Start is called before the first frame update
    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        padlock = GetComponent<BoxCollider2D>();
        currentRoom = this.transform.parent.gameObject.GetComponent<Room>();
        isLocked = false;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isclear = currentRoom.isCleared;
        Invoke("LockDoor", 1f);
        if (template.currentRoom == currentRoom)
        {
            Invoke("LockDoor",1f);
        }
        
    }


    private void LockDoor()
    {

        if (animator.GetBool("ForceLock") && currentRoom.isCleared == true)
        {
            print(1);
            isLocked = true;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = false;
        }
        else if (currentRoom.isCleared == false)
        {
            print(2);
            isLocked = true;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = false;
        }
        
        else
        {
            print(3);
            isLocked = false;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = true;
        }

    }

   



}
