using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    Entity entity;
    public bool onlyPlayOwnSounds;
    public string damageEvent;
    public string stunEvent;
    public string destroyEvent;
    public string[] usageEvent;
    public string[] pickupEvent;
    public string[] triggerEvents;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    void Start()
    {
        GameEvents.current.onDealDamage += OnDamage;
        GameEvents.current.onDestroyCommand += OnDestroyCommand;
        GameEvents.current.onPickup += OnPickup;
        GameEvents.current.onItemUsage += OnItemUsage;
        GameEvents.current.onStunEntity += OnStunEntity;
        GameEvents.current.onTrigger += OnTrigger;
    }


    private void OnDestroy()
    {
        GameEvents.current.onDealDamage -= OnDamage;
        GameEvents.current.onDestroyCommand -= OnDestroyCommand;
        GameEvents.current.onPickup -= OnPickup;
        GameEvents.current.onItemUsage -= OnItemUsage;
        GameEvents.current.onStunEntity -= OnStunEntity;
        GameEvents.current.onTrigger -= OnTrigger;
    }


    void OnDamage(int interactableID, int damageDealt)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;
        FMODUnity.RuntimeManager.PlayOneShot(damageEvent, transform.position);
    }


    void OnStunEntity(int interactableID, float stunPower)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        FMODUnity.RuntimeManager.PlayOneShot(stunEvent, transform.position);
    }


    void OnDestroyCommand(int interactableID)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        FMODUnity.RuntimeManager.PlayOneShot(destroyEvent, transform.position);
    }


    void OnItemUsage(int interactableID, NamedInt item)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        if (usageEvent == null || usageEvent.Length <= 0)
            return;

        foreach (string s in usageEvent)
        {
            if (s == "" || s == item.name)
                FMODUnity.RuntimeManager.PlayOneShot(s, transform.position);
        }

    }

    void OnPickup(int interactableID, SOItem item)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        if (pickupEvent == null || pickupEvent.Length <= 0)
            return;

        foreach (string s in pickupEvent)
        {
            if (s == "" || s == item.item.itemID)
                FMODUnity.RuntimeManager.PlayOneShot(s, transform.position);
        }

    }

    void OnTrigger(int interactableID, string triggerName)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        if (triggerEvents == null || triggerEvents.Length <= 0)
            return;

        foreach (string s in triggerEvents)
        {
            if (s == "" || s == triggerName)
                FMODUnity.RuntimeManager.PlayOneShot(s, transform.position);
        }

    }
}
