using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void SetUp()
    {
        Invoke("ActivateWinScreen", 0.5f);
    }

    void ActivateWinScreen()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        Globals.gameWon = false;
        Globals.isAlive = true;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
