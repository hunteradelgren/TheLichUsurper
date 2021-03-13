using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{

    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2d(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            Debug.Log("FoundBossRoom");
            sprite.color = new Color(1, 0, 0, 1);
        }


    }







}
