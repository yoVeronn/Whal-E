using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private float playerPosX;


    // Update is called once per frame
    void LateUpdate()
    {
        playerPosX = player.transform.position.x;
        transform.position = new Vector3((player.transform.position.x + 5f), transform.position.y, -10);
    }
}
