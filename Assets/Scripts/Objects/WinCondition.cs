using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public Entity entity;
    public TransitionController transition;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        transition.GetComponent<TransitionController>().StartCoroutine("BackToMenu");
    }

}
