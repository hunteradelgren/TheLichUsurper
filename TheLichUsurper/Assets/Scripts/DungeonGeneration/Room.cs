using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public int width = 12;
    public int height = 8;

    private RoomTemplate template;

    public bool isCleared;

    public List<GameObject> enemies= new List<GameObject>();
    public int enemyCount = 1;
    public GameObject item;
    public GameObject center;
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
        
        if (template.currentRoom == this)
        {
            if (enemyCount != 0)
                isCleared = false;
            else
                isCleared = true;
        }

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
        if (!droppedItem && isCleared && playerInRoom)
        {
            droppedItem = true;
            print("Spawning Item");
            GameObject pickup = Instantiate<GameObject>(item);
            pickup.transform.position = center.transform.position;
        }
    }







}
