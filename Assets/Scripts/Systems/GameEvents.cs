using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        if (current != null)
            return;

        current = this;
    }

    public event Action<int> onPlayerHealthUpdate;
    public void PlayerHealthUpdate(int currentHealth)
    {
        if (onPlayerHealthUpdate != null)
            onPlayerHealthUpdate(currentHealth);
    }


    public event Action<int, int> onDealDamage;
    public void DealDamage(int interactableID, int damageAmount)
    {
        if (onDealDamage != null)
            onDealDamage(interactableID, damageAmount);
    }


    public event Action<int, float> onStunEntity;
    public void StunEntity(int interactableID, float stunPower)
    {
        if (onStunEntity != null)
            onStunEntity(interactableID, stunPower);
    }


    public event Action<int, SOItem> onPickup;
    public void Pickup(int interactableID, SOItem item)
    {
        if (onPickup != null)
            onPickup(interactableID, item);
    }


    public event Action<int, NamedInt> onItemUsage;
    public void ItemUsage(int interactableID, NamedInt item)
    {
        if (onItemUsage != null)
            onItemUsage(interactableID, item);
    }


    public event Action<int> onDestroyCommand;
    public void DestroyCommand(int interactableID)
    {
        if (onDestroyCommand != null)
            onDestroyCommand(interactableID);
    }


    public event Action<int, string> onTrigger;
    public void Trigger(int interactableID, string triggerID)
    {
        if (onTrigger != null)
            onTrigger(interactableID, triggerID);
    }


    public event Action<Dictionary<string, Item>> onPlayerInventoryUpdate;
    public void PlayerInventoryUpdate(Dictionary<string, Item> inventory)
    {
        if (onPlayerInventoryUpdate != null)
            onPlayerInventoryUpdate(inventory);
    }
}
