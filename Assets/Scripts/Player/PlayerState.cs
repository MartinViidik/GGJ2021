using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateHandler
{
    public int maxHealth = 5;
    [Range(0f, 1f)] public float startBahPower = 0.5f;
    [Range(0f, 1f)] public float bahPerGrass = 0.2f;
    [Range(0f, 1f)] public float bahCosts = 0.3f;
    public Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    public float invincibleTime = 2f;
    float lastTimeDamageTake = float.MinValue;
    public Animator anim;

    int currentHealth;
    float currentBahPower;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            GameEvents.current.PlayerHealthUpdate(value);
        }
    }
    public float CurrentBahPower
    {
        get { return currentBahPower; }
        set
        {
            currentBahPower = value;
            Mathf.Clamp01(currentBahPower);
            GameEvents.current.PlayerBahUpdate(value);
        }
    }


    private void Start()
    {
        GameEvents.current.onDealDamage += OnDamage;
        GameEvents.current.onPickup += OnPickup;
        GameEvents.current.onItemUsage += OnItemUsage;
        GameEvents.current.onTrigger += OnTrigger;
        Invoke("SetupPlayer", 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDealDamage -= OnDamage;
        GameEvents.current.onPickup -= OnPickup;
        GameEvents.current.onItemUsage -= OnItemUsage;
        GameEvents.current.onTrigger -= OnTrigger;
    }

    void SetupPlayer()
    {
        CurrentHealth = maxHealth;
        CurrentBahPower = startBahPower;
    }

    private void Update()
    {
    }


    public bool CanIBah()
    {
        if (CurrentBahPower > bahCosts)
            return true;
        return false;
    }

    public void BahBeDone()
    {
        CurrentBahPower -= bahCosts;
    }


    void OnDamage(int interactableID, int damageDealt)
    {
        if (interactableID != entity.entityID)
            return;

        if (lastTimeDamageTake + invincibleTime > Time.time)
            return;

        lastTimeDamageTake = Time.time;
        CurrentHealth -= damageDealt;
        anim.Play("Damage");
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

    void OnTrigger (int interactableID, string triggerName)
    {
        if (interactableID != 0 && interactableID != entity.entityID)
            return;

        switch (triggerName)
        {
            case "BahUp":
                CurrentBahPower += bahPerGrass;
                break;
        }
    }
}
