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
    public bool status;
    public Animator animator;
    public playerStatsManager stats;
    public Image healthBar;
    //public Image healthBarBottom;
    public float invulnerable;
    public AudioClip beating;
    public AudioSource heartbeat;
    public AudioSource psource;
    public AudioClip laugh;
    public AudioClip scream;
    public Text liveText;
    public Text specText;

    public Color livecolor;
    public Color specColor;
    // Start is called before the first frame update

    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
        beating = heartbeat.clip;
        psource = GetComponent<AudioSource>();
        //starts the player off in living state with the associated max health 
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
        livecolor = new Color(1f, .5f, .5f, 1f);

        specColor = new Color(.75f, .7f, .7f, 1f);

        
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
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animator.SetTrigger("DiedSpectre");
            GetComponent<SpriteRenderer>().color = new Color(0.25f, .9f, 1f, .5f);
            
            Time.timeScale = 0;
            
            //player is dead so game will end
            
            
            StartCoroutine(dramaticDeathPause());
        }
        if(invulnerable>0)
        {
            invulnerable -= Time.deltaTime;
        }
        //if (Input.GetKeyDown(KeyCode.Space))//temp code to test damge taking things
        // takeDamage(1f);
    }
    IEnumerator dramaticDeathPause()
    {
        
        yield return new WaitForSecondsRealtime(5f);
        
        SceneManager.LoadScene(3);
    }
    public void playNo()
    {
        psource.clip = scream;
        psource.Play();
    }
    public void playLaugh()
    {
        psource.clip = laugh;
        psource.Play();
    }
    IEnumerator becomeSpectre()
    {
        heartbeat.Stop();
        
        Time.timeScale = 0;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("DiedLiving");
        print("Player has entered Spectral Form");
        //sets the player to spectral state
        inSpectralForm = true;
        currentHealth = stats.healthSM;
        healthBar.fillAmount = (currentHealth / maxSpectreHealth);
        
        
        liveText.text = "0/" + maxHealth;
        //makes character see thru, then waits
        healthBar.GetComponent<Image>().color = specColor;
        yield return new WaitForSecondsRealtime(3f);
        animator.updateMode = AnimatorUpdateMode.Normal;
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
                healthBar.fillAmount = (currentHealth / maxHealth);
                liveText.text = currentHealth + "/" + maxHealth;
            }
                        
             
            else
             {
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
            healthBar.fillAmount = (currentHealth / maxHealth);
            liveText.text = currentHealth + "/" + maxHealth;
            healthBar.GetComponent<Image>().color = livecolor;
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.fillAmount = (currentHealth / maxHealth);
            liveText.text = currentHealth + "/" + maxHealth;
            healthBar.GetComponent<Image>().color = livecolor;
        }
        stats.ItemPickedUp();

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

    public void healPercent(float percent)
    {
        float boost = maxHealth * percent;
        gainHealth(boost);
    }

    public void healthUpgrade(float amount)
    {
        stats.ItemPickedUp();
        maxHealth += amount;
        gainHealth(amount);
    }
    public void SetToSpectreColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(.25f, .9f, 1f, ((currentHealth - .5f) / maxSpectreHealth));
    }
}
