using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    //private float horizontalSpeedMin;
    //private float horizontalSpeedMax;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
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
        if (transform.position.x < playerTransform.position.x -20)
        {
            Destroy(gameObject);
        }
    }
}
