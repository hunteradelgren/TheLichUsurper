using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public AudioClip click;
    public AudioClip laugh;
    public AudioSource sounder;
    // Start is called before the first frame update
    void Start()
    {
        //sounder.clip = click;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PressStart()
    {
        sounder.Stop();
        sounder.clip = click;
        sounder.loop = false;
        sounder.Play();
        SceneManager.LoadScene(1);
    }
    public void PressQuit()
    {
        sounder.clip = laugh;
        sounder.Play();
        Application.Quit();
    }
    public void playSound()
    {//redundant method to be called in case the click sound needs to be played
        sounder.Play();
    }
}
