using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        anim.SetBool("MouseOverButton", true);
    }
    public void OnPointerExit()
    {
        anim.SetBool("MouseOverButton", false);
    }
}
