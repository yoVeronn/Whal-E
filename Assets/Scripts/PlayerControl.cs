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
    public static bool isGameActive = false;

    private MenuUIHandler menuUIHandler;
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject healthBar;
    private Slider slider;

    public Gradient healthBarGradient;
    public Image fill;

    private AudioSource playerAudio;
    public AudioClip dashSound;
    public AudioClip collisionSound;
    public AudioClip eatingSound;
    public AudioClip shipSound;
    public AudioClip checkpointSound;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true; 

        playerSpeed = 5.0f;

        menuUIHandler = GameObject.Find("Game Manager").GetComponent<MenuUIHandler>();

        slider = healthBar.GetComponent<Slider>();

        currentHealth = GameStatus.health;
        SetMaxHealth(maxHealth);

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        UpdateHealth();

        PlayerBounds();

        GameOverConditions();
    
    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement * Time.deltaTime * playerSpeed);

        //transform.rotation = Quaternion.LookRotation(movement);

        //dive on spacebar
        if (Input.GetKeyDown(KeyCode.Space) && diveActive == false)
        {
            playerAudio.PlayOneShot(dashSound, 0.8f);
            Diving();
        }
    }

    void UpdateHealth()
    {
        currentHealth = GameStatus.health;

        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }
    }

    void GameOverConditions()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isGameActive = false;
            menuUIHandler.GameOver();
        }

        if (O2Bar.currentO2 <= 0)
        {
            isGameActive = false;
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
        if (col.CompareTag("Enemy") && isGameActive)
        {
            Debug.Log("Health -20");
            //health decreases by 10
            GameStatus.health -= 20;
            SetHealth(GameStatus.health);
            //SFX: enemy collision SFX
            playerAudio.PlayOneShot(collisionSound, 0.8f);
        }

        else if (col.gameObject.CompareTag("Food") && isGameActive)
        {
            Debug.Log("Health +5");
            Destroy(col.gameObject);
            //health increases by 5
            GameStatus.health += 5;
            SetHealth(GameStatus.health);
            // SFX: eating SFX
            playerAudio.PlayOneShot(eatingSound, 0.3f);
        }

        else if (col.gameObject.CompareTag("Ship") && isGameActive)
        {
            Debug.Log("Health - 30");
            //health decreases by 30
            GameStatus.health -= 30;
            SetHealth(GameStatus.health);
            playerAudio.PlayOneShot(shipSound, 0.8f);
        }

        else if (col.gameObject.CompareTag("Checkpoint1"))
        {
            Destroy(col.gameObject);
            Debug.Log("Checkpoint");
            isGameActive = false;
            menuUIHandler.LoadNextLevel();

            playerAudio.PlayOneShot(checkpointSound, 0.6f);
        }

        else if (col.gameObject.CompareTag("Checkpoint2"))
        {
            Destroy(col.gameObject);
            Debug.Log("Checkpoint");
            isGameActive = false;
            menuUIHandler.LoadNextLevel();
            
            playerAudio.PlayOneShot(checkpointSound, 0.8f);
        }

        else if (col.gameObject.CompareTag("Checkpoint3"))
        {
            Destroy(col.gameObject);
            Debug.Log("Ending");
            isGameActive = false;
            menuUIHandler.TheEnd();

            playerAudio.PlayOneShot(checkpointSound, 0.8f);
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
