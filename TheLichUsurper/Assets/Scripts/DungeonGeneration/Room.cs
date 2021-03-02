using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public int width = 12;
    public int height = 8;

    private RoomTemplate template;



    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();

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
