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

    

    // Start is called before the first frame update
    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        padlock = GetComponent<BoxCollider2D>();
        currentRoom = this.transform.parent.gameObject.GetComponent<Room>();
        isLocked = false;
        animator = GetComponent<Animator>();
        padlock.isTrigger = !isLocked;
    }

    // Update is called once per frame
    void Update()
    {
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
            //print(1);//comment this line out unless using it for debugging
            isLocked = true;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = false;
        }
        else if (currentRoom.isCleared == false)
        {
            isLocked = true;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = false;
        }
        else
        {
            isLocked = false;
            animator.SetBool("isOpen", !isLocked);
            padlock.isTrigger = true;

        }

    }

   



}
