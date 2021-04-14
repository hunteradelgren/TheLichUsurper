using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damageRate = .5f;
    public float damage = 1;
    private float timer = 0;
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
        if (collision.tag == "Player")
        {
                timer = 0;
                collision.GetComponent<PlayerHealth>().takeDamage(damage);
        }

        if (collision.tag == "Enemy")
        {
            timer = 0;
            collision.GetComponent<EnemyHealth>().TakeTrapDamage(damage*.4f);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            timer += Time.deltaTime;
            if(timer >= damageRate)
            {
                timer = 0;
                collision.GetComponent<PlayerHealth>().takeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timer = 0;
        }
    }
}
