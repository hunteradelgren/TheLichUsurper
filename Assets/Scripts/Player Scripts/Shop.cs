using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public bool shopOpen = false;
    public playerStatsManager stats;
    public float moveTime;
    public GameObject shopping;
    public Text currentGold;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
        moveTime = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            shopping.SetActive(true);
            Time.timeScale = 0;
            shopOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && Time.timeScale == 0)
        {
            shopping.SetActive(false);
            Time.timeScale = moveTime;
            shopOpen = false;
        }
        if (shopOpen)
        {
            currentGold.text = "Current Gold: " + stats.currency;
        }
    }

    public void buyHealthRestore()
    {
        if(stats.currency >= 5)
        {
            stats.pHealth.gainHealth(10);
            stats.decreaseCurrency(5);
        }
    }
    public void buyHealthUpgrade()
    { 
        if(stats.currency >= 10)
        {
            stats.pHealth.healthUpgrade(5);
            stats.decreaseCurrency(10);
        }
    }
    public void buyMeleeUpgrade()
    {
        if(stats.currency >= 15)
        {
            stats.pMelee.damageUpgrade(1);
            stats.decreaseCurrency(15);
        }
    }
    public void buyRangeUpgrade()
    {
        if(stats.currency >= 15)
        {
            stats.pRange.damageUpgrade(1);
            stats.decreaseCurrency(15);
        }
    }
}
