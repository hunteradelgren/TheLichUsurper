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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //projectile continues along a straight path at the set speed
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.GetComponentInParent<PlayerHealth>() != null)
        {
            print("hit");
            collision.gameObject.GetComponentInParent<PlayerHealth>().takeDamage(damage);
            DestroyObject(gameObject);
        }
    }
    
}

