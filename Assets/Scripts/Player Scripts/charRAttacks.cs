using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charRAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;//time tracking field
    public float timeBetweenAttacks = .2f;//constant to track how long should be between each attack
    public float damage = 1;
    //public GameObject dummy;
    //[SerializeField] Transform heading;
    private GameObject projectilePrefab;
    [SerializeField]
    string projectileName;
    public Animator animator;


    private Quaternion temp;

    /// </summary>
    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>(projectileName);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;//tracks time elapsed
        if (Input.GetButton("Fire2") && timer >= timeBetweenAttacks && Time.timeScale != 0)//checks if enough time has elapsed when the fire button is clicked
        {
            timer = 0f;//resets the elapsed time and temporarily set the color of the head to red
                       // GameObject b = Instantiate(dummy, heading.transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
                       // b.GetComponent<basicProjectileBehavior>().isPlayerBullet = true;
                       //b.GetComponent<Rigidbody>().velocity = heading.transform.forward.normalized;

            animator.SetBool("IsCasting", true);
            print("is shooting");
            temp = Quaternion.Euler(0, 0, animator.GetFloat("PlayerRot"));
        }
        if (animator.GetBool("IsCasting") && animator.IsInTransition(0))
        {
            animator.SetBool("IsCasting", false);
        }
    }

    public void damageUpgrade(float amount)
    {
        damage += amount;
    }

    public void Shoot()
    {
        //instantiates the projectile
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.GetComponent<basicProjectileBehavior>().isPlayerBullet = true;
        projectile.GetComponent<basicProjectileBehavior>().damage = damage;
        //moves the projectile on top of the player
        projectile.transform.position = transform.position;
        //rotates projectile to where the player is facing
        projectile.transform.rotation = temp;
    }
}
