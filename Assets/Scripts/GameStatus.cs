using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static int health;
    public static int startingHealth;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
     

        if (health >= 100)
        {
            health = 100;
        }
    }
}
