using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class PlayerHealth : MonoBehaviour
{
    //max health while in a living state
    public float maxHealth;
    //max health in the undead state
    public float maxSpectreHealth;
    //current health regardless of state
    public float currentHealth;
    //true  if in undead state and false if in living state
    public bool inSpectralForm;
    //References to health Slider bars
    public Slider hpSlider;
    public Slider specSlider;
    public bool status;
    public Animator animator;
    public playerStatsManager stats;
    public Image healthBar;
    //public Image healthBarBottom;
    public float invulnerable;

    public AudioSource heartbeat;
    public AudioClip scream;
    public Text liveText;
    public Text specText;

    Color livecolor;
    Color specColor;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
        //starts the player off in living state with the associated max health 
        //inSpectralForm = false;
        //currentHealth = maxHealth;
        //sets live and spectre hp bars to current max values
        //hpSlider.maxValue = maxHealth;
        //specSlider.maxValue = maxSpectreHealth;
        //specSlider.value = maxSpectreHealth;
        //hpSlider.value = currentHealth;
        status = false;
        invulnerable = 0;
        if (inSpectralForm)
        {
            specText.text = stats.healthSC + "/" + stats.healthSM;
            liveText.text = 0 + "/" + stats.healthM;
            //print("Showing dead health");
        }
        else
        {
            specText.text = maxSpectreHealth + "/" + stats.healthSM;
            liveText.text = stats.healthC + "/" + stats.healthM;
            //print("Showing alive health");
        }
        livecolor = new Color(245, 101, 101, 255);
        specColor = new Color(160, 141, 141, 255);
    }

    // Update is called once per frame
    void Update()
    {
        //player has run out of health while in the living state
        if (currentHealth <= 0 && !inSpectralForm)
        {
            //starts timestop
            

            StartCoroutine(becomeSpectre());
            
        }
        //player has run out of health in the undead state
        else if (currentHealth <= 0 && inSpectralForm)
        {
            //player is dead so game will end
            Object.Destroy(gameObject);
            heartbeat.clip = scream;
            StartCoroutine(dramaticDeathPause());
            SceneManager.LoadScene(2);

        }
        if(invulnerable>0)
        {
            invulnerable -= Time.deltaTime;
        }
    }
    IEnumerator dramaticDeathPause()
    {
        yield return new WaitForSecondsRealtime(5f);
    }
    IEnumerator becomeSpectre()
    {
        heartbeat.Stop();
        Time.timeScale = 0;
        print("Player has entered Spectral Form");

        //sets the player to spectral state
        inSpectralForm = true;
        //currentHealth = specSlider.value;
        currentHealth = stats.healthSM;
        healthBar.fillAmount = (currentHealth / maxSpectreHealth);
        
            //healthBarBottom.fillAmount = (currentHealth / maxSpectreHealth);
        
        liveText.text = "0/" + maxHealth;
        //makes character see thru, then waits
        GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((currentHealth-.5f) / maxSpectreHealth));
        healthBar.GetComponent<Image>().color = specColor;
        //healthBarBottom.GetComponent<Image>().color = specColor;
        yield return new WaitForSecondsRealtime(1f);
        
        Time.timeScale = 1;
    }
   
    public void changeHealth()
    {
        status = !status;
        
        if (status)
        {
            maxHealth += 5;
            maxSpectreHealth += 5;
            currentHealth += 5;
            print("health upped");
        }
        else if (currentHealth > 5)
        {
            maxHealth -= 5;
            maxSpectreHealth -= 5;
            currentHealth -= 5;
            print("health downed");
        }
        if (!inSpectralForm)
        {
            hpSlider.maxValue = maxHealth;
            specSlider.maxValue = maxSpectreHealth;
            specSlider.value = maxSpectreHealth;
            hpSlider.value = currentHealth;
        }
        else
        {
            hpSlider.maxValue = maxHealth;
            specSlider.maxValue = maxSpectreHealth;
            specSlider.value = currentHealth;
        }
    }
    
    public void takeDamage(float damage)
    {
        animator.SetTrigger("wasHit");
        //decreases health by recieved amount
        
        if(invulnerable<=0)
        {  
             currentHealth -= damage;
             if (!inSpectralForm)
            {
                //hpSlider.value = currentHealth;
                healthBar.fillAmount = (currentHealth / maxHealth);
                liveText.text = currentHealth + "/" + maxHealth;
            }
                        
             
            else
             {
                //specSlider.value = currentHealth;
                specText.text = currentHealth + "/" + maxSpectreHealth;
                healthBar.fillAmount = (currentHealth / maxSpectreHealth);
                GetComponent<SpriteRenderer>().color = new Color(0.25f, .9f, 1f, ((currentHealth) / maxSpectreHealth));
             }

            invulnerable = .5f;
        }

        if(healthBar.fillAmount < .25f && !inSpectralForm)
        {
            print("Play1");
            heartbeat.Play();
        }
        else
        {
            print("Stop1");
            heartbeat.Stop();
        }
    }

    public void gainHealth(float boost)
    {
        if (inSpectralForm)
        {
            inSpectralForm = false;
            currentHealth = boost;
            //hpSlider.value = currentHealth;
            healthBar.fillAmount = (currentHealth / maxSpectreHealth);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 100);
            healthBar.GetComponent<Image>().color = livecolor;
            liveText.text = currentHealth + "/" + maxHealth;
        }
        else if (!inSpectralForm && currentHealth+boost <= maxHealth)
        {
            //increases health by received amount
            currentHealth += boost;
            //hpSlider.value = currentHealth;
            healthBar.fillAmount = (currentHealth / maxHealth);
            liveText.text = currentHealth + "/" + maxHealth;
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.fillAmount = (currentHealth / maxHealth);
            liveText.text = currentHealth + "/" + maxHealth;
        }

        if (healthBar.fillAmount < .25f && !inSpectralForm)
        {
            print("Play");
            heartbeat.Play();
        }
        else
        {
            print("Stop");
            heartbeat.Stop();
            
        }
    }

    public void healthUpgrade(float amount)
    {
        maxHealth += amount;
        hpSlider.maxValue = maxHealth;
        gainHealth(amount);
    }
}
