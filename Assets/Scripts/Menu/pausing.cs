using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausing : MonoBehaviour
{
    // Start is called before the first frame update
    ////public GameObject quitter;
    //public GameObject resumer;
    public GameObject managing;
    float moveTime;
    public AudioClip click;
    public AudioClip laugh;
    public AudioSource sounder;
    void Start()
    {
        //canvas = GetComponent<Canvas>();
        moveTime = Time.timeScale;
        sounder.clip = click;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            managing.SetActive(true);
            Time.timeScale = 0;
            print("paused");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            Resume();
    }
    public void Resume()
    {
        sounder.PlayOneShot(click);        
        print("unpaused");
        stopTime();
        managing.SetActive(false);
        Time.timeScale = moveTime;
    }
    IEnumerator stopTime()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
    public void Quit()
    {//quits the application if given the command
        sounder.PlayOneShot(laugh);
        stopTime();
        Application.Quit();
    }
}
