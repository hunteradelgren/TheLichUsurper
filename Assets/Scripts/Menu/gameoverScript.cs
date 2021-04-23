using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource sounder;
    public AudioClip click;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickToStart()
    {
        sounder.Stop();
        sounder.clip = click;
        sounder.loop = false;
        sounder.Play();
        SceneManager.LoadScene(0);
    }
}
