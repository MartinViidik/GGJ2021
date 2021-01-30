using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateHandler
{
    public int maxHealth = 5;
    public Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    public float invincibleTime = 2f;
    float lastTimeDamageTake = float.MinValue;

    int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            GameEvents.current.PlayerHealthUpdate(value);
        }
    }


    private void Start()
    {
        GameEvents.current.onDealDamage += OnDamage;
        GameEvents.current.onPickup += OnPickup;
        GameEvents.current.onItemUsage += OnItemUsage;
        Invoke("SetupPlayer", 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDealDamage -= OnDamage;
        GameEvents.current.onPickup -= OnPickup;
        GameEvents.current.onItemUsage -= OnItemUsage;
    }

    void SetupPlayer()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
    }


    void OnDamage(int interactableID, int damageDealt)
    {
        if (interactableID != entity.entityID)
            return;

        if (lastTimeDamageTake + invincibleTime > Time.time)
            return;

        lastTimeDamageTake = Time.time;
        CurrentHealth -= damageDealt;
    }


    void OnPickup(int interactableID, SOItem item)
    {
        if (interactableID != entity.entityID)
            return;

        if (inventory.ContainsKey(item.item.itemID))
        {
            inventory[item.item.itemID].usages += item.item.usages;
        } else
        {
            inventory.Add(item.item.itemID, new Item(item.item));
        }
        GameEvents.current.PlayerInventoryUpdate(inventory);
    }

    void OnItemUsage(int interactableID, NamedInt item)
    {
        if(inventory.ContainsKey(item.name))
        {
            inventory[item.name].usages -= item.value;
            if (inventory[item.name].usages == 0)
                inventory.Remove(item.name);
        }
        GameEvents.current.PlayerInventoryUpdate(inventory);
    }
}
