using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Entity entity;
    public Transform interactorAnchor;
    public float interactionRange = 3f;
    public PlayerState playerState;

    public GameObject tooltipPrefab;
    InteractableObject lastObjectInRange;

    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        if (xAxis != 0 || zAxis != 0)
            interactorAnchor.rotation = Quaternion.LookRotation(new Vector3(xAxis, 0 , zAxis));
    }

    private void Update()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("Interactable"));
        InteractableObject iObject = null;

        for (int i = 0; i < colliderList.Length && iObject == null; i++)
        {
            InteractableObject temp = colliderList[i].gameObject.GetComponent<InteractableObject>();
            if (temp != null && (temp.tag == "Enemy" || temp.tag == "Object") && temp.HasAction())
                iObject = colliderList[i].gameObject.GetComponent<InteractableObject>();
        }

        if (lastObjectInRange != iObject && lastObjectInRange != null)
        {
            lastObjectInRange.RemoveTooltip();
            lastObjectInRange = null;
        }

        if (iObject != null)
        {
            if (Input.GetButtonDown("Interact"))
                iObject.OnAction(entity.entityID, playerState.inventory);
            if (lastObjectInRange != iObject)
            {
                iObject.AddTooltip(tooltipPrefab, playerState.inventory);
                lastObjectInRange = iObject;
            }
        }
    }
}
