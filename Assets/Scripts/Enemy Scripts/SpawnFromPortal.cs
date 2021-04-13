using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromPortal : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject enemy;
    public string enemyName;
    public Room spawnroom = null;

    void Start()
    {
        enemy = Resources.Load<GameObject>(enemyName);
        //Spawnenemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnroom != null)
            Spawnenemy();
    }

    public void Spawnenemy()
    {
        GameObject e = Instantiate<GameObject>(enemy,transform.position,Quaternion.identity);
        e.GetComponent<EnemyMovement>().spawnRoom = spawnroom;
        destroyPortal();
    }
    public void destroyPortal()
    {
        DestroyObject(gameObject);
    }
}
