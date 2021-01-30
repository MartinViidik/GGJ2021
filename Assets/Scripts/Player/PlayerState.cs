using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateHandler
{
    public int maxHealth = 5;
    public Dictionary<string, SOItem> inventory = new Dictionary<string, SOItem>();

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
        Invoke("SetupPlayer", 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDealDamage -= OnDamage;
        GameEvents.current.onPickup -= OnPickup;
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

        if (inventory.ContainsKey(item.itemID))
        {
            inventory[item.itemID].usages += item.usages;
        } else
        {
            inventory.Add(item.itemID, item);
        }
        GameEvents.current.PlayerInventoryUpdate(inventory);
    }
}
