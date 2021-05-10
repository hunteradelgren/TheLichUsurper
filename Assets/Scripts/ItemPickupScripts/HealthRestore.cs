using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerHealth player;
    [SerializeField]
    float healthGain = 5;

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
        if (collision.tag == "Player")
        {
            player.healPercent(.3f);
            Object.Destroy(gameObject);
        }
    }
}
