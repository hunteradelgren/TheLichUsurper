using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    
    private float currentHealth;
    public Animator animator;

    public float HitStunTime;
    float hitStun;

    public bool IsStunned;
    bool hasDied = false;
    public AudioSource sound;
    public AudioClip hit;
    public AudioClip poof;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        hitStun = 0;
        IsStunned = false;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemy is out of health
        if(currentHealth <= 0)
        {
            if (!hasDied)
            {
                animator.SetTrigger("Died");
                hasDied = true;
            }
            else
            {
                return;
            }
        }
    
        if(hitStun>0)
        {
            IsStunned = true;
            hitStun -= Time.deltaTime;
        }
    
        else if(hitStun<=0)
        {
            IsStunned = false;
        }
    
    
    }

    public void TakeDamage(float amount)
    {
        if(!hasDied)
            sprite.color = new Color(1, 0, 0);
        currentHealth -= amount;
        animator.SetTrigger("wasHit");
        hitStun = HitStunTime;
        sound.PlayOneShot(hit);
    }
    public void TakeTrapDamage(float amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("wasHit");
        sound.PlayOneShot(hit);
    }
    public void HealDamage(float amount)
    {
        currentHealth += amount;
    }

    public bool GetIsStunned()
    {
        return IsStunned;
    }

    public void Death()
    {
        GameObject gem = Instantiate<GameObject>(Resources.Load<GameObject>("Gem"));
        gem.transform.position = transform.position;
        Destroy(gameObject);
    }

    public void playPoof()
    {
        sound.PlayOneShot(poof);
    }
    public void ResetColor()
    {
        sprite.color = new Color(1, 1, 1);
    }
}
