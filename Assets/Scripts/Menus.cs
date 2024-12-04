using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject menu;
    public GameObject ded;

    private bool Paused;

    private TurnHandler turnHandler;

    private void Start()
    {
        menu.SetActive(false);
        ded.SetActive(false);

        turnHandler = GetComponent<TurnHandler>();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        Paused = true;
    }

    public void PlayerDead()
    {
        menu.SetActive(true);
        ded.SetActive(true);
        Time.timeScale = 0;
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {
                Paused = false;
                menu.SetActive(false);
                Time.timeScale = 1;
            }

            else
            {
                Pause();
            }
        }
    }
}
