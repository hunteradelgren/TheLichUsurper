using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isBossDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Room>().enemies.Contains(GameObject.Find("BossHolder")) || collision.GetComponent<Room>().enemies.Contains(GameObject.Find("FinalBoss")))
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
