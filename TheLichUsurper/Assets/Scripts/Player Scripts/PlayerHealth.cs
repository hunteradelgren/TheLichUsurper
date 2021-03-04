using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class PlayerHealth : MonoBehaviour
{
    //max health while in a living state
    [SerializeField]
    float maxHealth;
    //max health in the undead state
    [SerializeField]
    float maxSpectreHealth;
    //current health regardless of state
    [SerializeField] float currentHealth;
    //true  if in undead state and false if in living state
    private bool inSpectralForm;
    //References to health Slider bars
    public Slider hpSlider;
    public Slider specSlider;
    public bool status;
    // Start is called before the first frame update
    void Start()
    {
        //starts the player off in living state with the associated max health 
        inSpectralForm = false;
        currentHealth = maxHealth;
        //sets live and spectre hp bars to current max values
        hpSlider.maxValue = maxHealth;
        specSlider.maxValue = maxSpectreHealth;
        specSlider.value = maxSpectreHealth;
        hpSlider.value = currentHealth;
        status = false;
    }

    // Update is called once per frame
    void Update()
    {
        //player has run out of health while in the living state
        if (currentHealth <= 0 && !inSpectralForm)
        {
            //will change the map and character to a more spectral look
            print("Player has entered Spectral Form");

            //sets the player to spectral state
            inSpectralForm = true;
            currentHealth = maxSpectreHealth;
        }
        //player has run out of health in the undead state
        else if (currentHealth <= 0 && inSpectralForm)
        {
            //player is dead so game will end
            print("Player has Died in Spectral Form");
            Object.Destroy(gameObject);
            SceneManager.LoadScene(2);

        }
        if (Input.GetButtonDown("Jump"))
            takeDamage(1f);

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
        //decreases health by recieved amount
        currentHealth -= damage;
        if (!inSpectralForm)
            hpSlider.value = currentHealth;
        else
            specSlider.value = currentHealth;

    }

    public void gainHealth(float boost)
    {
        //increases health by received amount
        currentHealth += boost;
        if (!inSpectralForm)
            hpSlider.value = currentHealth;
        else
            specSlider.value = currentHealth;
    }
}
