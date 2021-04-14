using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelpSignScript : MonoBehaviour
{
    public GameObject helpcanvas;
    public TextAsset helping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            helpcanvas.SetActive(true);
            helpcanvas.GetComponentInChildren<Text>().text = helping.ToString();
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            helpcanvas.SetActive(false);
            print("walking away from sign trigger");
        }
    }
}
