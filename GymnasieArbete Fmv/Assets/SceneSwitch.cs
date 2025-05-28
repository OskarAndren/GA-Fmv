using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchSceneToMovement ()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchSceneToAnimations()
    {
        SceneManager.LoadScene(2);
    }
    public void SwitchSceneToEnemies()
    {
        SceneManager.LoadScene(3);
    }
    public void SwitchSceneToKlarMedSpelet()
    {
        SceneManager.LoadScene(4);
    }
    public void SwitchSceneToMatsIntervju()
    {
        //SceneManager.LoadScene();
    }
}
