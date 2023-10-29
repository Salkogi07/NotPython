using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image img;

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameStop()
    {
        img.gameObject.SetActive(true);
    }

    public void GaemStopTab()
    {
        img.gameObject.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameMain()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameSave()
    {
        //game x
        //game y
        //story Id
        //story chat Id
        //emeny x
        //emeny y
    }


}
