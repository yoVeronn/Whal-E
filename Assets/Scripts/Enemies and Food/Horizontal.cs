using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    //private float horizontalSpeedMin;
    //private float horizontalSpeedMax;

    // Start is called before the first frame update
    void Start()
    {
        
        // UI: any SFX?
    }

    void Update()
    {
        EnemyMovement();
        DestroyOutofBounds();
    }

    // Update is called once per frame
    public virtual void EnemyMovement()
    {
        float horizontalSpeedMin = -10;
        float horizontalSpeedMax = -2;
      
        transform.Translate(Vector3.right * Time.deltaTime * Random.Range(horizontalSpeedMin, horizontalSpeedMax));

    }

    public void DestroyOutofBounds()
    {
        //Destroy out of bounds
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
