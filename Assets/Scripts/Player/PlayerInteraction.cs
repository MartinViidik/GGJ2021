using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InteractionHandler
{
    public float bahRange = 10f;
    public PlayerState playerState;


    [FMODUnity.EventRef]
    public string bahEvent;


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Object")
        {
            InteractableObject iObject = other.gameObject.GetComponent<InteractableObject>();
            if (iObject != null)
                iObject.OnTouch(entity.entityID, playerState.inventory);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Object")
        {
            InteractableObject iObject = other.gameObject.GetComponent<InteractableObject>();
            if (iObject != null)
                iObject.OnTouch(entity.entityID, playerState.inventory);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Bah"))
        {
            if (!playerState.CanIBah())
                return;

            playerState.BahBeDone();
            FMODUnity.RuntimeManager.PlayOneShot(bahEvent, transform.position);

            Collider[] colliderList = Physics.OverlapSphere(transform.position, bahRange, LayerMask.GetMask("Interactable"));
            foreach (Collider c in colliderList)
            {
                Debug.Log(c);
                if (c.tag == "Enemy" || c.tag == "Object")
                {
                    InteractableObject iObject = c.gameObject.GetComponent<InteractableObject>();
                    if (iObject != null)
                        iObject.OnBah(entity.entityID, playerState.inventory);
                }
            }
        }
    }
}
