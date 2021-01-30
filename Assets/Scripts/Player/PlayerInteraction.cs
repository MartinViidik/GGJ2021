using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionHandler
{
    public float bahRange = 10f;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Object")
        {
            InteractableObject iObject = other.gameObject.GetComponent<InteractableObject>();
            if (iObject != null)
                iObject.OnTouch(entity.entityID);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Bah"))
        {
            Collider[] colliderList = Physics.OverlapSphere(transform.position, bahRange, LayerMask.GetMask("Interactable"));
            foreach (Collider c in colliderList)
            {
                Debug.Log(c);
                if (c.tag == "Enemy" || c.tag == "Object")
                {
                    InteractableObject iObject = c.gameObject.GetComponent<InteractableObject>();
                    if (iObject != null)
                        iObject.OnBah(entity.entityID);
                }
            }
        }
    }
}
