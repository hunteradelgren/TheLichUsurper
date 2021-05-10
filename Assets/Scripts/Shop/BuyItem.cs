using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public int itemType;
//1 for restore 2 for Heath upgrade 3 for Melee Upgrade 4 for Ranged Upgrade

public playerStatsManager stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
        
    public void OnTriggerEnter2d(Collider other)
    {
     if(itemType == 1)
      {
         if (stats.currency >= 10)
         {
           stats.pHealth.healPercent(.3f);
           stats.decreaseCurrency(10);
         }
      }
      else if (itemType == 2)
         {
           if (stats.currency >= 20)
             {
               stats.pHealth.healthUpgrade(5);
               stats.decreaseCurrency(20);
            }
         }
      else if (itemType == 3)
      {
           if (stats.currency >= 35)
           {
            stats.pMelee.damageUpgrade(1);
            stats.decreaseCurrency(35);
           }
      }
      else if (itemType == 4)
      {
            if (stats.currency >= 35)
            {
             stats.pRange.damageUpgrade(1);
             stats.decreaseCurrency(35);
            }
      }
       
    }
}
