using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3 : GameManager
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
        float startDelay = 1.0f;
        float repeatRate = 2.0f;
        InvokeRepeating("SpawnHorizontalEntity", startDelay, repeatRate);
    }

    public override void SpawnHorizontalEntity()
    {
        Vector2 spawnPos = new Vector2(10, Random.Range(-4, 3.5f));
        int horizontalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[horizontalIndex], spawnPos, LevelPrefabs[horizontalIndex].transform.rotation);
    }
}
