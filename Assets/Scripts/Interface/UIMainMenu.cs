using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public TransitionController transition;
    public void StartGame()
    {
        transition.StartCoroutine("StartGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
