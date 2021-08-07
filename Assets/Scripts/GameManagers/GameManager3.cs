using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3 : GameManager
{
    public GameObject[] LevelPrefabsVertical;
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
        float repeatRate = 1.5f;
        InvokeRepeating("SpawnHorizontalEntity", startDelay, repeatRate);
        InvokeRepeating("SpawnVerticalEntity", startDelay, repeatRate);
    }

    public override void SpawnHorizontalEntity()
    {
        Vector2 spawnPos = new Vector2((player.transform.position.x + 18), Random.Range(-4, 3.5f));
        int horizontalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[horizontalIndex], spawnPos, LevelPrefabs[horizontalIndex].transform.rotation);
    }

    public override void SpawnVerticalEntity()
    {
        Vector2 spawnPos = new Vector2(Random.Range((player.transform.position.x - 1), (player.transform.position.x + 8)), 6);
        int verticalIndex = Random.Range(0, LevelPrefabsVertical.Length);
        Instantiate(LevelPrefabsVertical[verticalIndex], spawnPos, LevelPrefabsVertical[verticalIndex].transform.rotation);
    }
}
