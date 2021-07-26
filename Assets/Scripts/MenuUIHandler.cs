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

    public Animator transition;
    private float transitionTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        Time.timeScale = 1;
    }

    public void StartNew()
    {
        gameOverScreen.gameObject.SetActive(false);
        paused = false;
        Time.timeScale = 1;
        StartCoroutine(LoadLevel());
        SceneManager.LoadScene(1);
        GameStatus.health = 100;
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void LoadLevel2()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void LoadMenu()
    {
        gameOverScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else 
        Application.Quit();
#endif
    }

    void ChangePause()
    {
        if (!paused)
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }
}
