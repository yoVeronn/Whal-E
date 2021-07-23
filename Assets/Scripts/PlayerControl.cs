using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float playerSpeed;
    public bool diveActive = false;

    private MenuUIHandler menuUIHandler;
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject healthBar;
    private Slider slider;

    public Gradient healthBarGradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5.0f;

        menuUIHandler = GameObject.Find("Game Manager").GetComponent<MenuUIHandler>();

        slider = healthBar.GetComponent<Slider>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
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

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = healthBarGradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = healthBarGradient.Evaluate(1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Health -10");
            // UI: health decreases by 10
            currentHealth -= 10;
            SetHealth(currentHealth);
        }

        else if (col.gameObject.CompareTag("Food"))
        {
            Debug.Log("Health +5");
            Destroy(col.gameObject);
            // UI: health increases by 5
            currentHealth += 5;
            SetHealth(currentHealth);
            // SFX: eating SFX
        }

        else if (col.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Health - 30");
            // UI: health decreases by 30
            currentHealth -= 30;
            SetHealth(currentHealth);
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
