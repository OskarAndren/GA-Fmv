using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] WinScreen winScreen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetUp();
            Globals.gameWon = true;
        }
    }
}
