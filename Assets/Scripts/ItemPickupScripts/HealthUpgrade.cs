using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    private PlayerHealth player;
    [SerializeField]
    float healthGain = 5;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Object.Destroy(gameObject);
            player.healthUpgrade(healthGain);
            
        }
    }
}
