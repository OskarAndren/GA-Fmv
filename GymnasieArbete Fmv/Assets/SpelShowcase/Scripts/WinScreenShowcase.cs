using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenShowcase : MonoBehaviour
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
        SceneManager.LoadScene(4);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
