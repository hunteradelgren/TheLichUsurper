using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromPortal : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject enemy;
    public string enemyName;

    void Start()
    {
        enemy = Resources.Load<GameObject>(enemyName);
        Spawnenemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawnenemy()
    {
        GameObject e = Instantiate<GameObject>(enemy);
        e.transform.position = transform.position;
    }
}
