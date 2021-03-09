using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
    }

    public void HealDamage(float amount)
    {
        currentHealth += amount;
    }
}
