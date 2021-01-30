using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public Entity entity;

    void Awake()
    {
        entity = GetComponent<Entity>();
    }
}
