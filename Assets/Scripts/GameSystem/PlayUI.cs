using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject menuSet;
    public Image img;
    public Timer timer;

    private void Awake()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (GameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Resume()
    {
        img.gameObject.SetActive(false);
        menuSet.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        img.gameObject.SetActive(false);
        menuSet.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void diePlayer()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        timer.SetTimerStop();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Main");
    }
}
