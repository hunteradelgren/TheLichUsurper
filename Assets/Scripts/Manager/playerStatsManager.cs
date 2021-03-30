using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStatsManager : MonoBehaviour
{
    public PlayerHealth pHealth;
    public charMAttacks pMelee;
    public charRAttacks pRange;

    public float healthC = 10; //current health living
    public float healthSC = 10; //current spectre health
    public float healthM = 10; //max health
    public float healthSM = 10; //max spectre health
    public float meleeD = 1; //melee damage
    public float rangeD = 1; //range damage
    public bool inSpectre = false;

    public int currency = 0;

    static playerStatsManager manager;

    public bool levelchanged;
    public bool doneLoading;
    public bool first = true;
    public Scene cScene;

    // Start is called before the first frame update
    void Start()
    {
        if (manager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        doneLoading = false;
        
        manager = this;
        DontDestroyOnLoad(gameObject);


        pHealth = FindObjectOfType<PlayerHealth>();
        pMelee = FindObjectOfType<charMAttacks>();
        pRange = FindObjectOfType<charRAttacks>();
        pHealth.maxHealth = healthM;
        
        pHealth.maxSpectreHealth = healthSM;
        pMelee.damage = meleeD;
        pRange.damage = rangeD;

        pHealth.inSpectralForm = inSpectre;
        pHealth.specSlider.maxValue = healthSM;
        pHealth.specSlider.value = healthSC;
        pHealth.hpSlider.maxValue = healthM;
        if (!inSpectre)
        {
            pHealth.currentHealth = healthC;
            pHealth.hpSlider.value = healthC;
        }
        else
        {
            pHealth.currentHealth = healthSC;
            pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
            pHealth.hpSlider.value = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().Equals(cScene))
        {
            levelchanged = false;
        }
        else
        {
            levelchanged = true;
        }
        if (!doneLoading)
        {
            first = true;
        }
        if (!levelchanged && doneLoading && !first)
        {
            if (pHealth != null)
            {
                inSpectre = pHealth.inSpectralForm;
                if (!inSpectre)
                {
                    healthC = pHealth.currentHealth;
                    healthM = pHealth.maxHealth;
                }
                else
                {
                    healthSC = pHealth.currentHealth;
                }
            }
            if (pMelee != null)
            {
                meleeD = pMelee.damage;
            }
            if (pRange != null)
            {
                rangeD = pRange.damage;
            }
        }
        else if(pHealth != null)
        {
            pHealth.maxHealth = healthM;
            pHealth.maxSpectreHealth = healthSM;
            pMelee.damage = meleeD;
            pRange.damage = rangeD;

            pHealth.inSpectralForm = inSpectre;
            pHealth.specSlider.maxValue = healthSM;
            pHealth.specSlider.value = healthSC;
            pHealth.hpSlider.maxValue = healthM;
            if (!inSpectre)
            {
                pHealth.currentHealth = healthC;
                pHealth.hpSlider.value = healthC;
            }
            else
            {
                pHealth.currentHealth = healthSC;
                pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
                pHealth.hpSlider.value = 0f;
            }
            cScene = SceneManager.GetActiveScene();
            first = false;
        }
        else
        {
            pHealth = FindObjectOfType<PlayerHealth>();
            pMelee = FindObjectOfType<charMAttacks>();
            pRange = FindObjectOfType<charRAttacks>();
            pHealth.maxHealth = healthM;
            
            pHealth.maxSpectreHealth = healthSM;
            pMelee.damage = meleeD;
            pRange.damage = rangeD;

            pHealth.inSpectralForm = inSpectre;
            pHealth.specSlider.maxValue = healthSM;
            pHealth.specSlider.value = healthSC;
            pHealth.hpSlider.maxValue = healthM;
            if (!inSpectre)
            {
                pHealth.currentHealth = healthC;
                pHealth.hpSlider.value = healthC;
            }
            else
            {
                pHealth.currentHealth = healthSC;
                pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
                pHealth.hpSlider.value = 0f;
            }
            cScene = SceneManager.GetActiveScene();
            first = false;
        }
    }

    public void increaseCurrency(int change)
    {
        currency += change;
    }

    public void decreaseCurrency(int change)
    {
        currency -= change;
    }
}
