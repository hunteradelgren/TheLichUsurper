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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        hitStun = 0;
        IsStunned = false;
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
        currentHealth -= amount;
        animator.SetTrigger("wasHit");
        hitStun = HitStunTime;
    }
    public void TakeTrapDamage(float amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("wasHit");
        
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
        Destroy(gameObject);
    }

}
