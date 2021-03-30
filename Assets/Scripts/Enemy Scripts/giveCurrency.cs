using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveCurrency : MonoBehaviour
{
    // Start is called before the first frame update
    public int currencyToGive = 1;
    public playerStatsManager stats;

    private float wait = 0;

    void Start()
    {
        stats = FindObjectOfType<playerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wait <= 2)
        {
            wait += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        if(wait>=2)
        stats.increaseCurrency(currencyToGive);
    }
}
