using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public Entity entity;
    void Awake()
    {
        entity = GetComponent<Entity>();
    }


    public enum InteractionType { None, Touch, Action, Bah }
    public enum TargetType { Self, Interactor }


    public DamageInteraction[] damageInteractions;
    public StunInteraction[] stunInteractions;


    public virtual void OnTouch(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Touch);
    }

    public virtual void OnAction(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Action);
    }

    public virtual void OnBah(int interactionID)
    {
        CheckBasics(interactionID, InteractionType.Bah);
    }

    public void CheckBasics(int interactionID, InteractionType interactionType)
    {
        DealDamage(interactionID, interactionType);
        StunEntity(interactionID, interactionType);
    }


    // ---- Other stuff
    void DealDamage(int interactionID, InteractionType interactionType)
    {
        foreach (DamageInteraction i in damageInteractions)
        {
            if (interactionType == i.interactionType)
            {
                switch (i.targetType)
                {
                    case TargetType.Interactor:
                        GameEvents.current.DealDamage(interactionID, i.damageAmount);
                        break;
                    case TargetType.Self:
                        GameEvents.current.DealDamage(entity.entityID, i.damageAmount);
                        break;
                }
            }
        }
    }


    void StunEntity(int interactionID, InteractionType interactionType)
    {
        foreach (StunInteraction i in stunInteractions)
        {
            if (interactionType == i.interactionType)
            {
                switch (i.targetType)
                {
                    case TargetType.Interactor:
                        GameEvents.current.StunEntity(interactionID, i.stunPower);
                        break;
                    case TargetType.Self:
                        GameEvents.current.StunEntity(entity.entityID, i.stunPower);
                        break;
                }
            }
        }
    }
}


[Serializable]
public struct DamageInteraction
{
    public InteractableObject.InteractionType interactionType;
    public InteractableObject.TargetType targetType;
    public int damageAmount;
}

[Serializable]
public struct StunInteraction
{
    public InteractableObject.InteractionType interactionType;
    public InteractableObject.TargetType targetType;
    public float stunPower;
}