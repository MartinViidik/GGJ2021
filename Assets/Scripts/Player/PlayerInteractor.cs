using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Entity entity;
    public Transform interactorAnchor;
    public float interactionRange = 3f;


    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        if (xAxis != 0 || zAxis != 0)
            interactorAnchor.rotation = Quaternion.LookRotation(new Vector3(xAxis, 0 , zAxis));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Collider[] colliderList = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("Interactable"));
            foreach (Collider c in colliderList)
            {
                Debug.Log(c);
                if (c.tag == "Enemy" || c.tag == "Object")
                {
                    InteractableObject iObject = c.gameObject.GetComponent<InteractableObject>();
                    if (iObject != null)
                        iObject.OnAction(entity.entityID);
                }
            }
        }
    }
}
