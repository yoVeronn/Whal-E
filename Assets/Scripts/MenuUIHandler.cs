using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseScreen;

    public GameObject gameOverScreen;
    public GameObject EndScreen;

    public static bool isGameActive = false;

    public Animator transition;
    private float transitionTime = 2;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1));
    }

    public void Restart()
    {
        Time.timeScale = 1;
        gameOverScreen.gameObject.SetActive(false);

        // start from the same level as death
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GameStatus.health = GameStatus.startingHealth;
            // rewind to that health value when level needs to be replayed.
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        gameOverScreen.gameObject.SetActive(false);

        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int SceneIndex)
    {
        Time.timeScale = 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
        isGameActive = true;

        paused = false;

        if (SceneIndex == 1)
        {
            GameStatus.health = 100;
        }
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else 
        Application.Quit();
#endif
    }

    public void ChangePause()
    {
        if (!paused) //&&isGameActive == true;
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TheEnd()
    {
        EndScreen.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }
}
