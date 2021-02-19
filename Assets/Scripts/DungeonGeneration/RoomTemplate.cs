using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplate : MonoBehaviour
{
    public GameObject[] bottomDoorRooms;
    public GameObject[] topDoorRooms;
    public GameObject[] leftDoorRooms;
    public GameObject[] rightDoorRooms;

    public int roomRangeMin;
    public int roomRangeMax;
    private GameObject[] roomCount;

    public Room currentRoom;

    public CameraMechanics camera;

    void Start()
    {
        Invoke("DifficultyTester", 1f); 

    }


    private void DifficultyTester()
    {
        

        roomCount = GameObject.FindGameObjectsWithTag("Room");

        if(roomCount.Length < roomRangeMin || roomCount.Length > roomRangeMax)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }


    public void OnPlayerEnterRoom(Room room)
    {
        camera.currentRoom = room;
        currentRoom = room;

    }



}
