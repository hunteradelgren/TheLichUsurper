using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromPortal : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject enemy;
    public string enemyName;
    public Room spawnroom = null;
    public Animator portal;
    public AudioSource sound;
    public AudioClip open;
    public AudioClip spin;

    void Start()
    {
        portal = GetComponent<Animator>();
        enemy = Resources.Load<GameObject>(enemyName);
        //Spawnenemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawnenemy()
    {
        GameObject e = Instantiate<GameObject>(enemy, transform.position, Quaternion.identity, transform.parent);
        e.GetComponent<PortalEnemyMovement>().spawnRoom = spawnroom;
        if (e.GetComponent<PortalRangedAttack>())
            e.GetComponent<PortalRangedAttack>().spawnRoom = spawnroom;
        spawnroom.updateEnemyList();
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
        sound.PlayOneShot(spin);
    }
}

