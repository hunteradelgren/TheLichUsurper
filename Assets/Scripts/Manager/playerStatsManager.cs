using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerStatsManager : MonoBehaviour
{
    public PlayerHealth pHealth;
    public charMAttacks pMelee;
    public charRAttacks pRange;

    public float healthC = 10; //current health living
    public float healthSC = 10; //current spectre health
    public float healthM = 10; //max health
    public float healthSM = 10; //max spectre health
    public float meleeD = 2; //melee damage
    public float rangeD = 1; //range damage
    public bool inSpectre = false;

    public int currency = 0;
    public Text currencyText;
    static playerStatsManager manager;

    public bool levelchanged;
    public bool doneLoading;
    public bool first = true;
    public Scene cScene;

    public AudioSource sound;
    public AudioClip itemPickup;
    public AudioClip gemPickup;

    // Start is called before the first frame update
    void Awake()
    {
        if (manager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        doneLoading = false;
        
        manager = this;
        DontDestroyOnLoad(gameObject);
        sound = GetComponent<AudioSource>();
        currencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<Text>();
        pHealth = FindObjectOfType<PlayerHealth>();
        pMelee = FindObjectOfType<charMAttacks>();
        pRange = FindObjectOfType<charRAttacks>();
        pHealth.maxHealth = healthM;
        
        pHealth.maxSpectreHealth = healthSM;
        pMelee.damage = meleeD;
        pRange.damage = rangeD;

        pHealth.inSpectralForm = inSpectre;
        if (!inSpectre)
        {
            pHealth.currentHealth = healthC;
        }
        else
        {
            pHealth.currentHealth = healthSC;
            pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
        }
        currencyText.text = "X " + currency;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))//temp code to test currency changes to text output
            increaseCurrency(1);*/
        if (SceneManager.GetActiveScene().name == "GameOverScene" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            Object.Destroy(gameObject);
        }

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
            if (!inSpectre)
            {
                pHealth.currentHealth = healthC;
            }
            else
            {
                pHealth.currentHealth = healthSC;
                pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
            }
            cScene = SceneManager.GetActiveScene();
            first = false;
        }
        else
        {
            pHealth = FindObjectOfType<PlayerHealth>();
            pMelee = FindObjectOfType<charMAttacks>();
            pRange = FindObjectOfType<charRAttacks>();
            currencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<Text>();
            currencyText.text = "X " + currency;
            if (pHealth != null)
            {
                pHealth.maxHealth = healthM;
                
                pHealth.maxSpectreHealth = healthSM;
                pMelee.damage = meleeD;
                pRange.damage = rangeD;

                pRange.display.text = pRange.damage.ToString();
                pMelee.display.text = pMelee.damage.ToString();

                pHealth.inSpectralForm = inSpectre;

                if (!inSpectre)
                {
                    pHealth.currentHealth = healthC;
                    pHealth.healthBar.fillAmount = healthC / healthM;
                }
                else
                {
                    pHealth.currentHealth = healthSC;
                    pHealth.healthBar.fillAmount = healthSC / healthSM;
                    pHealth.GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((pHealth.currentHealth - .5f) / pHealth.maxSpectreHealth));
                    pHealth.healthBar.color = pHealth.specColor;
                }
                cScene = SceneManager.GetActiveScene();
                first = false;
            }
        }
    }

    public void increaseCurrency(int change)
    {
        sound.PlayOneShot(gemPickup);
        currency += change;
        if (currencyText != null)
            currencyText.text = "X " + currency;
    }

    public void decreaseCurrency(int change)
    {
        currency -= change;
        currencyText.text = "X " + currency;
    }

    public void resetStats()
    {//added this method so the player's stats reset when called
        healthC = 10; //current health living
        healthSC = 10; //current spectre health
        healthM = 10; //max health
        healthSM = 10; //max spectre health
        meleeD = 1; //melee damage
        rangeD = 1; //range damage
        inSpectre = false;

        currency = 0;
    }

    public void ItemPickedUp()
    {
        sound.PlayOneShot(itemPickup);
    }
}
