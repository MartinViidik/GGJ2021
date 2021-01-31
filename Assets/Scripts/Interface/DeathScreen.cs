using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public TransitionController transition;

    public void Start()
    {
        gameObject.SetActive(false);
        GameEvents.current.onPlayerDead += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        gameObject.SetActive(true);
    }

    public void StartGame()
    {
        transition.gameObject.SetActive(true);
        transition.StartCoroutine("StartGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        transition.gameObject.SetActive(true);
        transition.StartCoroutine("BackToMenu");
    }
}
