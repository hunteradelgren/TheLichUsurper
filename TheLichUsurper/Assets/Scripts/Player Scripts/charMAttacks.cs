using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;//time tracking field
    public float timeBetweenAttacks = .2f;//constant to track how long should be between each attack
    public float damage = 1;
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;//tracks time elapsed
        if (Input.GetButton("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)//checks if enough time has elapsed when the fire button is clicked
        {
            timer = 0f;//resets the elapsed time and temporarily set the color of the head to red
            GetComponentInChildren<SpriteRenderer>().color = new Color(255,0,0);
        }

        if (timer >= timeBetweenAttacks * .2f)
        {//sets the color of the head back to black to show the end of the attack
            GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0);
        }
    }
    public void damageUpgrade(float amount) 
    {
        damage += amount;
    }

}
