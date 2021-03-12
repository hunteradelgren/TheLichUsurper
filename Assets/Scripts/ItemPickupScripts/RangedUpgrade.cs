using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUpgrade : MonoBehaviour
{
    private charRAttacks player;
    [SerializeField]
    float damageGain = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<charRAttacks>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            player.damageUpgrade(damageGain);
        }
    }
}
