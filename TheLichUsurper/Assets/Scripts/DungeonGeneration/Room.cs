using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public int width = 12;
    public int height = 8;

    private RoomTemplate template;

    public int WeightForDelete;

    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        WeightForDelete = Random.Range(0, 100);

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







}
