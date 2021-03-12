using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    
    private float currentHealth;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemy is out of health
        if(currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("wasHit");
    }

    public void HealDamage(float amount)
    {
        currentHealth += amount;
    }
}
