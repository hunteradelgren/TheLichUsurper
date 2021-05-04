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

    public bool hit;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

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
        if (!hit)
        {
            //projectile continues along a straight path at the set speed
            transform.position += transform.right.normalized * projectileSpeed * Time.deltaTime;
        }
        else
        {
            anim.SetBool("Hit", true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall" || collision.transform.GetComponent<Door>() != null || collision.transform.tag == "Obstacle")
        {
            hit = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null && !isPlayerBullet)
        {
            hit = true;
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        else if (isPlayerBullet && collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            hit = true;
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        else if (isPlayerBullet && collision.gameObject.GetComponent<FirstBoss>() != null)
        {
            hit = true;
            collision.gameObject.GetComponent<FirstBoss>().takeDamage(damage);
        }
        else if (isPlayerBullet && collision.gameObject.GetComponent<FinalBoss>() != null)
        {
            hit = true;
            collision.gameObject.GetComponent<FinalBoss>().TakeDamage(damage);
        }
        if (collision.transform.tag == "Wall" || collision.transform.GetComponent<Door>() != null || collision.transform.tag == "Obstacle")
        {
            hit = true;
        }

        if (collision.transform.tag == "Playerprojectile" || collision.transform.tag == "projectile")
        {
            hit = true;
        }
    }

    public void destroyProjectile()
    {
        GameObject.Destroy(gameObject);
    }

    /*void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.gameObject.GetComponentInParent<PlayerHealth>() == null && collision.gameObject.GetComponent<basicProjectileBehavior>() == null)
        {
            GameObject.Destroy(gameObject);
        }

    }*/
}