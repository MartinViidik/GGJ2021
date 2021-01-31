using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TransitionController : MonoBehaviour
{
    public Image image;
    public int SceneToLoad;
    public float TransitionSpeed;

    bool ShouldReveal = false;

    void Start()
    {
        GetComponent<Image>();
        image.material.SetFloat("_CutOff", 1);
        ShouldReveal = true;
    }

    private void Update()
    {
        if (!ShouldReveal)
        {
            image.material.SetFloat("_CutOff", Mathf.MoveTowards(image.material.GetFloat("_CutOff"), 1.1f, TransitionSpeed * Time.deltaTime));
        } else {
            image.material.SetFloat("_CutOff", Mathf.MoveTowards(image.material.GetFloat("_CutOff"), -0.1f, TransitionSpeed * Time.deltaTime));
        }
    }

    public IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(1f);
        ShouldReveal = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        ShouldReveal = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }


}
