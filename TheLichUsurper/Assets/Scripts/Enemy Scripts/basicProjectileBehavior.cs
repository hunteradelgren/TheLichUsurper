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
        transform.position += transform.forward.normalized * projectileSpeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<PlayerHealth>() != null && !isPlayerBullet)
        {
            //print("hit");
            collision.gameObject.GetComponentInParent<PlayerHealth>().takeDamage(damage);
            DestroyObject(gameObject);
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (isPlayerBullet && collision.gameObject.GetComponentInParent<PlayerHealth>() == null)
        {
            print("exiting " + collision.gameObject.name);
            DestroyObject(gameObject);
        }
    }
}