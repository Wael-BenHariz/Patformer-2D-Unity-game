using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenus : MonoBehaviour
{
    [SerializeField] string mainMenu;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject gameOver;

    bool gameIsPaused;

    public Animator transition;
    public float transtionTime = 1f;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pause.GetComponent<Canvas>().enabled = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pause.GetComponent<Canvas>().enabled = true;
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevel(mainMenu));
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOver.GetComponent<Canvas>().enabled = true;
    }

    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        SceneManager.LoadScene(level);
    }
}