using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicProjectileBehavior : MonoBehaviour
{
    //sets the speed of the projectile
    [SerializeField]
    float projectileSpeed;

    public float damage;

    public bool isPlayerBullet = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //projectile continues along a straight path at the set speed
        transform.position += transform.right.normalized * projectileSpeed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall" || collision.transform.GetComponent<Door>() != null)
        {
            GameObject.Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerHealth>() != null && !isPlayerBullet)
        {
            GameObject.Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        else if (isPlayerBullet && collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            GameObject.Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        else if (isPlayerBullet && collision.gameObject.GetComponent<FirstBoss>() != null)
        {
            GameObject.Destroy(gameObject);
            collision.gameObject.GetComponent<FirstBoss>().TakeDamage(damage);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.gameObject.GetComponentInParent<PlayerHealth>() == null && collision.gameObject.GetComponent<basicProjectileBehavior>() == null)
        {
            GameObject.Destroy(gameObject);
        }

    }
}