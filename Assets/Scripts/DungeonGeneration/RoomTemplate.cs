using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomTemplate : MonoBehaviour
{
    // sets the arrays to hold the room prefabs
    public GameObject[] bottomDoorRooms;
    public GameObject[] topDoorRooms;
    public GameObject[] leftDoorRooms;
    public GameObject[] rightDoorRooms;

    //allows for scalable dungeon size to increase difficulty
    public int roomRangeMin;
    public int roomRangeMax;

    //makesd an array for the rooms in order to chjeck if its in the paramaters
    private GameObject[] roomCount;
    private GameObject[] EndRooms;
    //private GameObject[] BottomRooms;

    //holds the boss room and shop room
    public GameObject boss;
    public GameObject shop;

    //loading objects
    public GameObject startButton;
    public GameObject LoadCanv;
    public Text LoadLabel;
    
    //player object
    public GameObject player;
    public playerStatsManager pStats;
    public Text currencyText;
    public Room currentRoom;
    
    
    public CameraMechanics cam;
    public AudioSource sounder;
    public AudioClip clip;
    Time time;
    void Start()
    {
        //sets the player Stats manager
        pStats = FindObjectOfType<playerStatsManager>();
        pStats.doneLoading = false;
        //player.SetActive(false);
        LoadCanv.SetActive(true);
        //Time.timeScale = 0;
        sounder = GetComponent<AudioSource>();
        
        //checks to see if the dungeon has the required amount of rooms after 1.5 seconds
        Invoke("DifficultyTester", 1.5f); 

    }


    private void DifficultyTester() //compares totals rooms to min max values and rebuilds if there are too many or too few rooms, also sets the boss and shop room code
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
        //sets boss room
        Instantiate(boss, EndRooms[EndRooms.Length - 1].transform.position, Quaternion.identity);
        Destroy(EndRooms[EndRooms.Length - 1]);

        EndRooms = GameObject.FindGameObjectsWithTag("TopRoom");

        if (EndRooms.Length == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        //sets shop room
        Instantiate(shop, EndRooms[EndRooms.Length - 1].transform.position, Quaternion.identity);
        Destroy(EndRooms[EndRooms.Length - 1]);

        print("Loaded");
        pStats.doneLoading = true;
        StartCoroutine(delay());
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        startButton.SetActive(true);
        LoadLabel.text = "Loaded";
    }

    public void OnPlayerEnterRoom(Room room) //changes camera when player enters the room
    {
        cam.currentRoom = room;
        currentRoom = room;

    }

    public void goLevel()
    {
        pStats.currencyText = currencyText;
        pStats.currencyText.text = "X " + pStats.currency;
        //player.SetActive(true);
        sounder.PlayOneShot(clip);
        player.GetComponent<charMAttacks>().started = true;
        player.GetComponent<charRAttacks>().started = true;
        player.GetComponent<Movement>().started = true;
        LoadCanv.SetActive(false);
    }
}
