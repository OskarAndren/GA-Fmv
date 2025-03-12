using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenShowcase : MonoBehaviour
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
