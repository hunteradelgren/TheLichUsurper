using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject quitter;
    public GameObject resumer;
    float moveTime;
    void Start()
    {
        //canvas = GetComponent<Canvas>();
        moveTime = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            quitter.SetActive(true);
            resumer.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            Resume();
    }
    public void Resume()
    {
        quitter.SetActive(false);
        resumer.SetActive(false);
        Time.timeScale = moveTime;
    }
    public void Quit()
    {//quits the application if given the command
        Application.Quit();
    }
}
