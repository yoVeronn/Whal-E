using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public GameObject pauseScreen;
    public bool paused = false;

    public GameObject gameOverScreen;
    public bool isGameActive = false;

    public GameObject EndScreen;

    public Animator transition;
    private float transitionTime = 2;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
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

        StartCoroutine(LoadLevel(1));
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        gameOverScreen.gameObject.SetActive(false);

        isGameActive = true;
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int SceneIndex)
    {
        Time.timeScale = 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
        
        paused = false;

        if (SceneIndex == 1)
        {
            GameStatus.health = 100;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
       // Time.timeScale = 0;
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
