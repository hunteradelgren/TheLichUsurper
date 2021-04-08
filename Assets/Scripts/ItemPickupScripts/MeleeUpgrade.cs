using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUpgrade : MonoBehaviour
{
    private charMAttacks player;
    [SerializeField]
    float damageGain = .4;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<charMAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            player.damageUpgrade(damageGain);
        }
    }
}
