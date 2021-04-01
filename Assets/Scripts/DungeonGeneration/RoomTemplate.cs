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
    public GameObject startButton;
    public GameObject LoadCanv;
    public GameObject player;
    public playerStatsManager pStats;

    public Room currentRoom;

    public CameraMechanics cam;
    Time time;
    void Start()
    {
        pStats = FindObjectOfType<playerStatsManager>();
        pStats.doneLoading = false;
        player.SetActive(false);
        LoadCanv.SetActive(true);
        //Time.timeScale = 0;
        Invoke("DifficultyTester", 1.5f); 

    }


    private void DifficultyTester()
    {

        //Time.timeScale = 0;
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
        print("Loaded");
        pStats.doneLoading = true;
        StartCoroutine(delay());
        startButton.SetActive(true);
    }
    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        cam.currentRoom = room;
        currentRoom = room;

    }

    public void goLevel()
    {
        player.SetActive(true);
        LoadCanv.SetActive(false);
    }
}
