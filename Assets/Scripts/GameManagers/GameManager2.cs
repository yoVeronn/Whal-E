using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : GameManager
{

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpawnEntity()
    {
        float startDelay = 2.0f;
        float repeatRate = 1f;
        InvokeRepeating("SpawnHorizontalEntity", startDelay, repeatRate);
    }
}
