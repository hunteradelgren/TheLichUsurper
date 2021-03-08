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
    private GameObject[] EndRooms;
    public GameObject boss;
    public GameObject curtain;

    public Room currentRoom;

    public CameraMechanics cam;

    void Start()
    {
        Invoke("DifficultyTester", 1.5f); 

    }


    private void DifficultyTester()
    {
        

        roomCount = GameObject.FindGameObjectsWithTag("Room");

        if(roomCount.Length < roomRangeMin || roomCount.Length > roomRangeMax)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }


        EndRooms = GameObject.FindGameObjectsWithTag("BottomRoom");

         if (EndRooms.Length == 0)
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
        
        Instantiate(boss, EndRooms[EndRooms.Length - 1].transform.position, Quaternion.identity);
        
        Destroy(EndRooms[EndRooms.Length - 1]);
        curtain.SetActive(false);
    }


    public void OnPlayerEnterRoom(Room room)
    {
        cam.currentRoom = room;
        currentRoom = room;

    }

   

}
