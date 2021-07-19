using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Level1EnemyPrefabs;
    private float startDelay = 2.0f;
    private float repeatRate = 3; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        InvokeRepeating("SpawnVerticalEnemy", startDelay, repeatRate);
       // InvokeRepeating("SpawnHorizontalEnemy", startDelay, Random.Range(minRepeatRate, maxRepeatRate));
    }

    void SpawnVerticalEnemy()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-8, 8), 6);
        int verticalIndex = Random.Range(0, Level1EnemyPrefabs.Length);
        Instantiate(Level1EnemyPrefabs[verticalIndex], spawnPos, Level1EnemyPrefabs[verticalIndex].transform.rotation);
    }

    //void SpawnHorizontalEnemy()
    //{
    //    int horizontalIndex = Random.Range(0, 1);
    //    Instantiate(Level1EnemyPrefabs[verticalIndex], new Vector3(Random.Range(-8, 8), 6, 0), Level1EnemyPrefabs[horizontalIndex].transform.rotation);
    //}
}
