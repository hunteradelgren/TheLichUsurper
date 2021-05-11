using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignEnabler : MonoBehaviour
{
    public RoomTemplate template;

    // Start is called before the first frame update
    void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (template.currentRoom.enemyCount > 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
