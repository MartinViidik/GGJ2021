using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum InteractionType { None, Touch, Action, Bah }

    [Header("Damage Settings")]
    public InteractionType damageInteractionType;
    public int dealtDamage;



    public virtual void OnTouch(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Touch);
    }

    public virtual void OnAction(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Touch);
    }

    public virtual void OnBah(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Touch);
    }

    public void CheckBasics(int interactionID, InteractionType interactionType)
    {
        DealDamage(interactionID, interactionType);
    }


    // ---- Other stuff
    void DealDamage(int interactionID, InteractionType interactionType)
    {
        if (interactionType == damageInteractionType)
            GameEvents.current.DealDamage(interactionID, dealtDamage);
    }
}