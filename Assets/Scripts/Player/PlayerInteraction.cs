using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionHandler
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Object")
        {
            InteractableObject iObject = other.gameObject.GetComponent<InteractableObject>();
            if (iObject != null)
                iObject.OnTouch(entity.entityID);
        }
    }
}
