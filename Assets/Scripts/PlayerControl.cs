using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float playerSpeed;
    public bool diveActive = false;

    private MenuUIHandler menuUIHandler;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5.0f;
        menuUIHandler = GameObject.Find("Game Manager").GetComponent<MenuUIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * playerSpeed);

        // player bounds
        PlayerBounds();

        //dive on spacebar
        if (Input.GetKeyDown(KeyCode.Space) && diveActive == false)
        {
            Diving();
        }
    
    }

    void PlayerBounds()
    {
        if (transform.position.y < -4.4f)
        {
            transform.position = new Vector2(transform.position.x, -4.4f);
        }

        else if (transform.position.y > 4.4f)
        {
            transform.position = new Vector2(transform.position.x, 4.4f);
        }

        else if (transform.position.x < -8.3f)
        {
            transform.position = new Vector2(-8.3f, transform.position.y);
        }
    }

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

        else if (col.gameObject.CompareTag("Checkpoint1"))
        {
            Debug.Log("Checkpoint");
            menuUIHandler.LoadLevel2();
        }

        else if (col.gameObject.CompareTag("Checkpoint2"))
        {
            Debug.Log("Checkpoint");
            menuUIHandler.LoadLevel3();
        }

        else if (col.gameObject.CompareTag("Checkpoint3"))
        {
            Debug.Log("Ending");
            //menuUIHandler.End scene();
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
