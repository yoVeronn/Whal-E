using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] LevelPrefabs;
    // UI: sound
    // UI: buttons for pause (and restart?)
    public GameObject gameOver;
    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntity();
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if player health = 0, game over
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOver.gameObject.SetActive(true);
    }

    public virtual void SpawnEntity()
    {
        float startDelay = 2.0f;
        float repeatRate = 3;
        InvokeRepeating("SpawnVerticalEntity", startDelay, repeatRate);
    }

    void SpawnVerticalEntity()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-8, 8), 6);
        int verticalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[verticalIndex], spawnPos, LevelPrefabs[verticalIndex].transform.rotation);
    }

    public virtual void SpawnHorizontalEntity()
    {
        Vector2 spawnPos = new Vector2(10, Random.Range(-4, 4));
        int horizontalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[horizontalIndex], spawnPos, LevelPrefabs[horizontalIndex].transform.rotation);
    }
}
