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
    public AudioClip click;
    public AudioSource sounder;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
        moveTime = Time.timeScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shopOpen)
        {
            currentGold.text = "Current Gems: " + stats.currency;
        }
    }

    public void openShopMenu()
    {
        shopping.SetActive(true);
        Time.timeScale = 0;
        shopOpen = true;
    }

    public void closeShopMenu()
    {
        shopping.SetActive(false);
        Time.timeScale = moveTime;
        sounder.PlayOneShot(click);
        shopOpen = false;
    }

    IEnumerator stopTime()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    public void buyHealthRestore()
    {
        if(stats.currency >= 10)
        {
            stats.pHealth.healPercent(.3f);
            stats.decreaseCurrency(10);
            Time.timeScale = moveTime;
            sounder.PlayOneShot(click);
            stopTime();
            Time.timeScale = 0;
        }
    }
    public void buyHealthUpgrade()
    { 
        if(stats.currency >= 20)
        {
            stats.pHealth.healthUpgrade(5);
            stats.decreaseCurrency(20);
            Time.timeScale = moveTime;
            sounder.PlayOneShot(click);
            stopTime();
            Time.timeScale = 0;
        }
    }
    public void buyMeleeUpgrade()
    {
        if(stats.currency >= 35)
        {
            stats.pMelee.damageUpgrade(1);
            stats.decreaseCurrency(35);
            Time.timeScale = moveTime;
            sounder.PlayOneShot(click);
            stopTime();
            Time.timeScale = 0;
        }
    }
    public void buyRangeUpgrade()
    {
        if(stats.currency >= 35)
        {
            stats.pRange.damageUpgrade(1);
            stats.decreaseCurrency(35);
            Time.timeScale = moveTime;
            sounder.PlayOneShot(click);
            stopTime();
            Time.timeScale = 0;
        }
    }
}
