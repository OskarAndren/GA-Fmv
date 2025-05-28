using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchSceneToMovement ()
    {
        Debug.Log("entered scene switch");
        SceneManager.LoadScene(2);
    }
    public void SwitchSceneToAnimations()
    {
        SceneManager.LoadScene(3);
    }
    public void SwitchSceneToEnemies()
    {
        SceneManager.LoadScene(4);
    }
    public void SwitchSceneToKlarMedSpelet()
    {
        SceneManager.LoadScene(5);
    }
    public void SwitchSceneToMatsIntervju()
    {
        //SceneManager.LoadScene();
    }
}
