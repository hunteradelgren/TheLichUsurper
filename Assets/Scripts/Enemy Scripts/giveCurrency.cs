using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveCurrency : MonoBehaviour
{
    // Start is called before the first frame update
    public int currencyToGive = 1;
    public playerStatsManager stats;

    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        stats.increaseCurrency(currencyToGive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHealth>() != null)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
