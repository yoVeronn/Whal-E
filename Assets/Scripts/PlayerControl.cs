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

        currentHealth = GameStatus.health;
        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = GameStatus.health;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        //transform.Translate(Vector3.up * verticalInput * Time.deltaTime * playerSpeed);

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement * Time.deltaTime * playerSpeed);

        //transform.rotation = Quaternion.LookRotation(movement);

        // player bounds
        PlayerBounds();

        //dive on spacebar
        if (Input.GetKeyDown(KeyCode.Space) && diveActive == false)
        {
            Diving();
        }

        // game over
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            menuUIHandler.GameOver();
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
        slider.value = GameStatus.health;

        fill.color = healthBarGradient.Evaluate(slider.normalizedValue);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Health -20");
            //health decreases by 10
            GameStatus.health -= 20;
            SetHealth(GameStatus.health);
            //SFX: enemy collision SFX
        }

        else if (col.gameObject.CompareTag("Food"))
        {
            Debug.Log("Health +5");
            Destroy(col.gameObject);
            //health increases by 5
            GameStatus.health += 5;
            SetHealth(GameStatus.health);
            // SFX: eating SFX
        }

        else if (col.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Health - 30");
            //health decreases by 30
            GameStatus.health -= 30;
            SetHealth(GameStatus.health);
        }

        else if (col.gameObject.CompareTag("Checkpoint1"))
        {
            Destroy(col.gameObject);
            Debug.Log("Checkpoint");
           // Time.timeScale = 0;
            menuUIHandler.LoadNextLevel();
        }

        else if (col.gameObject.CompareTag("Checkpoint2"))
        {
            Destroy(col.gameObject);
            Debug.Log("Checkpoint");
            menuUIHandler.LoadNextLevel();
        }

        else if (col.gameObject.CompareTag("Checkpoint3"))
        {
            Destroy(col.gameObject);
            Debug.Log("Ending");
            menuUIHandler.TheEnd();
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
