using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width = 12;
    public int height = 8;

    private RoomTemplate template;

    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
    }


    public Vector3 GetRoomCenter()
    {
        /*GameObject temp = GameObject.FindGameObjectWithTag("Center");


        return temp.transform.position;*/

    }





    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision");
            template.OnPlayerEnterRoom(this);
        }

    }







}
