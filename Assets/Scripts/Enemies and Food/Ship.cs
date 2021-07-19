using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Horizontal
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        DestroyOutofBounds();
    }

    public override void EnemyMovement()
    {
        float horizontalSpeedMin = -5;
        float horizontalSpeedMax = -2;
        
        transform.Translate(Vector3.right * Time.deltaTime * Random.Range(horizontalSpeedMin, horizontalSpeedMax));
    }
}
