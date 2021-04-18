using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charMAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;//time tracking field
    public float timeBetweenAttacks = .2f;//constant to track how long should be between each attack
    public GameObject attackBox;
    public float damage;//Now relies totally on the reference in the public inspector
    public Animator animator;
    public GameObject target;
    public bool hitSomething;
    public AudioClip sword;
    public AudioSource sound;
    public Text display;
    public bool started;
    /// </summary>
    void Start()
    {
        sound = GetComponent<AudioSource>();
        display.text = ("MA = " + damage);
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer += Time.deltaTime;//tracks time elapsed
            if (Input.GetButton("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)//checks if enough time has elapsed when the fire button is clicked
            {
                timer = 0f;//resets the elapsed time and temporarily set the color of the head to red
                           //GetComponentInChildren<SpriteRenderer>().color = new Color(255,0,0);
                animator.SetBool("IsAttacking", true);


            }

            if (animator.GetBool("IsAttacking") && animator.IsInTransition(0))
            {
                animator.SetBool("IsAttacking", false);
                //GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255);
            }
        }
    }
    public void damageUpgrade(float amount) 
    {
        damage = damage + amount;
        display.text = ("MA = " + damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>() != null)
            target = collision.gameObject;
        if (collision.gameObject.GetComponent<FirstBoss>() != null)
            target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision.gameObject)
        {
            target = null;
        }
    }
    public void SwingSword()
    {
        if (target.GetComponent<EnemyHealth>() != null)
        {
            target.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        if (target.GetComponent<FirstBoss>() != null)
        {
            target.GetComponent<FirstBoss>().takeDamage(damage);
        }
        if (target.GetComponent<FinalBoss>() != null)
        {
            target.GetComponent<FinalBoss>().TakeDamage(damage);
        }
    }
    public void playSwordSound()
    {
        sound.clip = sword;
        sound.PlayOneShot(sword,1);
    }
}
