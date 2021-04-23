using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachShop : MonoBehaviour
{
    public Shop menu;
    public bool entered = false;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        menu = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!entered)
            {
                menu.openShopMenu();
                anim.SetBool("CloseSkull", false);
                entered = true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetBool("CloseSkull", true);
            entered = false;
        }
    }
}
