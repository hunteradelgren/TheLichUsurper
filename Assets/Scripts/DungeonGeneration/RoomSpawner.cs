using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDir;
    //1 needs a room to spawn with a bottom door
    //2 is a top door
    //3 is a left door
    //4 is a right door

    


    private bool spawned = false;
    
    public RoomTemplate template;
    
    int rand;
    
    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", .1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(spawned == false)
        {
            if(openingDir == 1)
             {
                rand = Random.Range(0, template.bottomDoorRooms.Length);
                Instantiate(template.bottomDoorRooms[rand], transform.position, Quaternion.identity);

             }

            if (openingDir == 2)
            {
                rand = Random.Range(0, template.topDoorRooms.Length);
                Instantiate(template.topDoorRooms[rand], transform.position, Quaternion.identity);

            }

            if (openingDir == 3)
            {
                rand = Random.Range(0, template.leftDoorRooms.Length);
                Instantiate(template.leftDoorRooms[rand], transform.position, Quaternion.identity);

            }

            if (openingDir == 4)
            {
                rand = Random.Range(0, template.rightDoorRooms.Length);
                Instantiate(template.rightDoorRooms[rand], transform.position, Quaternion.identity);

            }

             spawned = true;
        }
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            Destroy(this);
        }
        

    }




}
