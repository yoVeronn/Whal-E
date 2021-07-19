using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVertical : MonoBehaviour
{
    private float verticalSpeedMin = -8;
    private float verticalSpeedMax = -2;
    public GameObject player;   

    // Start is called before the first frame update
    void Start()
    {
        //UI: play water splash SFX
    }

    // Update is called once per frame
    void Update()
    {     
        // enemy movement
        transform.Translate(Vector3.up * Time.deltaTime * Random.Range(verticalSpeedMin, verticalSpeedMax));
        //Destroy out of bounds
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
