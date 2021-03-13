using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherLocked : MonoBehaviour
{
    private Animator animator;
    private List<GameObject> colliding = new List<GameObject>();
    public bool a;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print(colliding.Count);
        a = animator.GetBool("ForceLock");
        if(colliding.Count > 0)
        {
            animator.SetBool("ForceLock", false);
            
        }
        else
        {
            animator.SetBool("ForceLock", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Door>() != null)
        {
            colliding.Add(collision.gameObject);
            print(colliding);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Door>() != null)
        {
            colliding.Add(collision.gameObject);
            print(colliding);
        }
    }
}
