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

    public float lifetime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=lifetime)
            Destroy(gameObject);
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
            Destroy(gameObject);
        }
    }
    
}

