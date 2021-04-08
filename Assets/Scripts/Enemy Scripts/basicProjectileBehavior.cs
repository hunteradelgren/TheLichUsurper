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


    public AudioSource sound;
    public AudioClip fire;
    public float volume = 1f;
    public bool loopSound = false;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        sound.clip = fire;
        if (loopSound)
            sound.Play();
        else
        {
            sound.PlayOneShot(fire, volume);
        }
            
    }
    private void Awake()
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
        if (collision.transform.tag == "Wall" || collision.transform.GetComponent<Door>() != null || collision.transform.tag == "Obstacle")
        {
            GameObject.Destroy(gameObject);
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null && !isPlayerBullet)
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
        if (collision.transform.tag == "Wall" || collision.transform.GetComponent<Door>() != null || collision.transform.tag == "Obstacle")
        {
            GameObject.Destroy(gameObject);
        }

        if (collision.transform.tag == "Playerprojectile")
        {
            GameObject.Destroy(gameObject);
        }


    }

    /*void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.gameObject.GetComponentInParent<PlayerHealth>() == null && collision.gameObject.GetComponent<basicProjectileBehavior>() == null)
        {
            GameObject.Destroy(gameObject);
        }

    }*/
}