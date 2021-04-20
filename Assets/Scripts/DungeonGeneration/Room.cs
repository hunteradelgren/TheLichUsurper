using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    //sets room scale
    public int width = 12;
    public int height = 8;

    //hold the dungeon controller
    private RoomTemplate template;

    //checks if a room has been cleared
    public bool isCleared;

    public List<GameObject> enemies= new List<GameObject>();
    public int enemyCount = 1;
    
    //holds the powerup object
    public GameObject item;
    public GameObject center;
    
    //checks if a powerup has been drooped
    public bool droppedItem;
    public bool playerInRoom;

    void Start()
    {
        playerInRoom = false;
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        isCleared = true;
        droppedItem = false;
        foreach (Transform child in this.transform)
        {
            if (child.tag == "Enemy")
                enemies.Add(child.gameObject);
        }

    }


    void Update()
    {
        enemyCount = enemies.Count;
        
        //checks to see if player is in this room and is there are any enemies
        if (template.currentRoom == this)
        {
            if (enemyCount != 0)
                isCleared = false;
            else
                isCleared = true;
        }

        //removes dead enemies from the array
        foreach(GameObject enemy in enemies)
        {
            if(enemy == null)
            {
                enemies.Remove(enemy);
            }
        }

        if (template.currentRoom == this)
            playerInRoom = true;
        
        SpawnItem();

    }
   
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           template.OnPlayerEnterRoom(this);
        }
        if (other.tag == "Room")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }
    public void SpawnItem()
    {
        //drops item when all enemies are dead
        if (!droppedItem && isCleared && playerInRoom)
        {
            droppedItem = true;
            //print("Spawning Item");
            GameObject pickup = Instantiate<GameObject>(item);
            pickup.transform.position = center.transform.position;
        }
    }

    public void updateEnemyList()
    {
        enemies = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            if (child.tag == "Enemy")
                enemies.Add(child.gameObject);
        }
    }







}
