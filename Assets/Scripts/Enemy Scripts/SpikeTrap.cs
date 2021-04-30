using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damageRate = 1.5f;
    public float damage = 1;
    private float timer = 0;

    public bool isEnabled = true;

    public List<GameObject> Colliding;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Colliding = new List<GameObject>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            timer += Time.deltaTime;
            if (Colliding.Count > 0 && timer >= damageRate)
            {
                timer = 0;
                anim.SetTrigger("Extend");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            Colliding.Add(collision.gameObject);
        }
    }

    public void dealDamage()
    {
        foreach(GameObject p in Colliding)
        {
            if(p.tag == "Player")
            {
                p.GetComponent<PlayerHealth>().takeDamage(damage);
            }
            else if(p.tag == "Enemy")
            {
                p.GetComponent<EnemyHealth>().TakeTrapDamage(damage * .4f);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            Colliding.Remove(collision.gameObject);
        }
    }
}
