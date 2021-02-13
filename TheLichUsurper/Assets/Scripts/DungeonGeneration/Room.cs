using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;

    public int Height;

    public int X;


    public int Y;


    void Start()
    {
        Debug.Log(X);
        if (RoomController.instance == null)
        {
            Debug.Log("Loaded in the wrong scene");
            return;

        }
        Debug.Log("Registering");
        RoomController.instance.RegisterRoom(this);

    }


    public Vector3 GetRoomCenter()
    {

        return new Vector3(X * Width, Y * Height);

    }

    void OnTriggerEnter(Collider other)
    {

            Debug.Log("Collided");
            RoomController.instance.OnPlayerEnterRoom(this);
        

    }



}
