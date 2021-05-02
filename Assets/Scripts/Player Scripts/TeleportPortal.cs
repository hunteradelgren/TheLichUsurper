using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportPortal : MonoBehaviour
{
    // Start is called before the first frame update

    public Room spawnroom = null;
    public Animator portal;
    public AudioSource sound;
    public AudioClip open;
    public AudioClip spin;

    void Awake()
    {
        //portal = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            collision.gameObject.SetActive(false);
            portal.SetTrigger("spinoff");
        }
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void DestroyPortal()
    {
        Object.Destroy(gameObject);
    }
    public void playOpen()
    {
        sound.PlayOneShot(open);
    }
    public void playSpin()
    {
        if (GetComponentInParent<Room>() == FindObjectOfType<RoomTemplate>().currentRoom)
            sound.PlayOneShot(spin);
        else
            sound.Stop();
    }
}

