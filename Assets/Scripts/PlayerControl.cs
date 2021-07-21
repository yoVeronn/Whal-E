using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float playerSpeed;
    public bool diveActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * playerSpeed);

        // player bounds

        //dive on spacebar, cooldown of 4s
        if (Input.GetKeyDown(KeyCode.Space) && diveActive == false)
        {
            Diving();
        }
    
    }

    //IEnumerator DiveCountdownRoutine()
    //{
    //    yield return new WaitForSeconds(4);
    //    diveActive = false;
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Health -10");
            // UI: health decreases by 10
        }

        else if (col.gameObject.CompareTag("Food"))
        {
            Debug.Log("Health +5");
            Destroy(col.gameObject);
            // UI: health increases by 5
            // SFX: eating SFX
        }

        else if (col.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Health - 30");
            // UI: health decreases by 30
        }
    }

    void Diving()
    {
        playerSpeed *= 2;
        diveActive = true;
        StartCoroutine(DivingDuration());
    }

    IEnumerator DivingDuration()
    {
        yield return new WaitForSeconds(0.5f);
        playerSpeed = 5.0f;
        diveActive = false;

    }
}
