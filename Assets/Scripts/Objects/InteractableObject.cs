using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public Entity entity;
    public string interactionTooltip = "press [E] to interact";
    public string missingRequirementTip = "missing item";
    public Vector3 tooltipPosition = new Vector3(0, 1, 0);
    GameObject tooltip;

    void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void Start()
    {
        GameEvents.current.onDestroyCommand += SelfDestroy;
    }

    private void OnDestroy()
    {
        GameEvents.current.onDestroyCommand -= SelfDestroy;
    }

    private void SelfDestroy(int interactableID)
    {
        if (interactableID != entity.entityID)
            return;

        Destroy(gameObject);
    }



    public enum InteractionType { None, Touch, Action, Bah }
    public enum TargetType { Self, Interactor, Any }

    public bool onlyOneUsage = false;
    bool objectUsed = false;

    public NamedInt[] requiredItems;

    public DamageInteraction[] damageInteractions;
    public StunInteraction[] stunInteractions;
    public PickupInteraction[] pickupInteractions;
    public DestroyInteraction[] destroyInteractions;
    public TriggerInteraction[] triggerInteractions;

    public void AddTooltip(GameObject prefab, Dictionary<string, Item> inventory)
    {
        RemoveTooltip();
        if (onlyOneUsage && objectUsed)
            return;
        tooltip = Instantiate(prefab, transform);
        tooltip.transform.localPosition = tooltipPosition;
        if (CheckRequirements(inventory))
            tooltip.GetComponent<InteractableTooltip>().SetTooltip(interactionTooltip);
        else
            tooltip.GetComponent<InteractableTooltip>().SetTooltip(missingRequirementTip);
    }

    public void RemoveTooltip()
    {
        if (tooltip != null)
            Destroy(tooltip);
    }

    public bool HasAction()
    {
        foreach (DamageInteraction i in damageInteractions)
            if (i.interactionType == InteractionType.Action) return true;

        foreach (StunInteraction i in stunInteractions)
            if (i.interactionType == InteractionType.Action) return true;

        foreach (PickupInteraction i in pickupInteractions)
            if (i.interactionType == InteractionType.Action) return true;

        foreach (DestroyInteraction i in destroyInteractions)
            if (i.interactionType == InteractionType.Action) return true;

        foreach (TriggerInteraction i in triggerInteractions)
            if (i.interactionType == InteractionType.Action) return true;

        return false;
    }

    public virtual void OnTouch(int interactionID, Dictionary<string, Item> inventory)
    {
        if (!CheckRequirements(inventory))
            return;
        Item.UseItems(interactionID, requiredItems);

        CheckBasics(interactionID, InteractionType.Touch);
    }

    public virtual void OnAction(int interactionID, Dictionary<string, Item> inventory)
    {
        if (!CheckRequirements(inventory))
            return;
        Item.UseItems(interactionID, requiredItems);

        CheckBasics(interactionID, InteractionType.Action);
        objectUsed = true;
        RemoveTooltip();
    }

    public virtual void OnBah(int interactionID, Dictionary<string, Item> inventory)
    {
        if (!CheckRequirements(inventory))
            return;
        Item.UseItems(interactionID, requiredItems);

        CheckBasics(interactionID, InteractionType.Bah);
    }

    bool CheckRequirements(Dictionary<string, Item> inventory)
    {
        if (onlyOneUsage && objectUsed)
            return false;

        if (!Item.ContainsAllItems(inventory, requiredItems))
            return false;
        return true;
    }


    public void CheckBasics(int interactionID, InteractionType interactionType)
    {
        DealDamage(interactionID, interactionType);
        StunEntity(interactionID, interactionType);
        Pickup(interactionID, interactionType);
        DestroyCommand(interactionID, interactionType);
        TriggerEvent(interactionID, interactionType);
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


    void Pickup(int interactionID, InteractionType interactionType)
    {
        foreach (PickupInteraction i in pickupInteractions)
        {
            if (interactionType == i.interactionType)
            {
                switch (i.targetType)
                {
                    case TargetType.Interactor:
                        GameEvents.current.Pickup(interactionID, i.item);
                        break;
                    case TargetType.Self:
                        GameEvents.current.Pickup(entity.entityID, i.item);
                        break;
                }
            }
        }
    }


    void DestroyCommand(int interactionID, InteractionType interactionType)
    {
        foreach (DestroyInteraction i in destroyInteractions)
        {
            if (interactionType == i.interactionType)
            {
                switch (i.targetType)
                {
                    case TargetType.Interactor:
                        GameEvents.current.DestroyCommand(interactionID);
                        break;
                    case TargetType.Self:
                        GameEvents.current.DestroyCommand(entity.entityID);
                        break;
                }
            }
        }
    }


    void TriggerEvent(int interactionID, InteractionType interactionType)
    {
        foreach (TriggerInteraction i in triggerInteractions)
        {
            if (interactionType == i.interactionType)
            {
                switch (i.targetType)
                {
                    case TargetType.Interactor:
                        GameEvents.current.Trigger(interactionID, i.triggerName);
                        break;
                    case TargetType.Self:
                        GameEvents.current.Trigger(entity.entityID, i.triggerName);
                        break;
                    case TargetType.Any:
                        GameEvents.current.Trigger(0, i.triggerName);
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


[Serializable]
public struct PickupInteraction
{
    public InteractableObject.InteractionType interactionType;
    public InteractableObject.TargetType targetType;
    public SOItem item;
}


[Serializable]
public struct DestroyInteraction
{
    public InteractableObject.InteractionType interactionType;
    public InteractableObject.TargetType targetType;
}


[Serializable]
public struct TriggerInteraction
{
    public InteractableObject.InteractionType interactionType;
    public InteractableObject.TargetType targetType;
    public string triggerName;
}