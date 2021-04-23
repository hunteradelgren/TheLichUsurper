using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public AudioSource sounder;
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() 
    {
        sounder.Stop();
        sounder.clip = click;
        sounder.loop = false;
        sounder.Play();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        sounder.Stop();
        sounder.clip = click;
        sounder.loop = false;
        sounder.Play();
        SceneManager.LoadScene(0);
    }
}
