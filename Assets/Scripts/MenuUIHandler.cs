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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNew()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
