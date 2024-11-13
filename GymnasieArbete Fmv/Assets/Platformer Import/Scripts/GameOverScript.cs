using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public void SetUp()
    {
            Invoke("ActivateScreen", 1f);
    }

    private void ActivateScreen()
    {
        gameObject.SetActive(true);
    }
}
