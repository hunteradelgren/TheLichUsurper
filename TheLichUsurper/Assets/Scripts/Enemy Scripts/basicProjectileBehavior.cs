using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicProjectileBehavior : MonoBehaviour
{
    //sets the speed of the projectile
    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float damage;

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

        if (collision.gameObject.GetComponentInParent<PlayerHealth>() != null && !isPlayerBullet)
        {
            print("hit");
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            GameObject.Destroy(gameObject);
        }
        if (collision.transform.tag == "Wall")
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.gameObject.GetComponentInParent<PlayerHealth>() == null)
        {
            GameObject.Destroy(gameObject);
        }

    }
}