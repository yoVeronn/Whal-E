using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] LevelPrefabs;
    // UI: sound
    public GameObject player;
    public GameObject gameOver;
    public bool isGameActive = false;

    // public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntity();
        isGameActive = true;
        // playerHealth = 100;
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

    public virtual void SpawnVerticalEntity()
    {
        Vector2 spawnPos = new Vector2(Random.Range((player.transform.position.x -2), (player.transform.position.x+ 18)), 6);
        int verticalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[verticalIndex], spawnPos, LevelPrefabs[verticalIndex].transform.rotation);
    }

    public virtual void SpawnHorizontalEntity()
    {
        Vector2 spawnPos = new Vector2(player.transform.position.x + 15, Random.Range((player.transform.position.y - 4), (player.transform.position.y + 4)));
        int horizontalIndex = Random.Range(0, LevelPrefabs.Length);
        Instantiate(LevelPrefabs[horizontalIndex], spawnPos, LevelPrefabs[horizontalIndex].transform.rotation);
    }
}
