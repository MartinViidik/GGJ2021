using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public Entity entity;
    // Start is called before the first frame update
    private void Start()
    {
        entity = GetComponent<Entity>();
        GameEvents.current.onTrigger += OnTrigger;
    }

    void OnTrigger(int interactableID, string triggerID)
    {
        Debug.Log("GameWin");
    }

}
