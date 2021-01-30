using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public Entity entity;
    public bool onlyPlayOwnSounds;

    [FMODUnity.EventRef]
    public string damageEvent;
    float lastTimeDamage = float.MinValue;
    float timeBetweenDamage = 2f;

    [FMODUnity.EventRef]
    public string stunEvent;

    [FMODUnity.EventRef]
    public string destroyEvent;

    public NamedFmodSound[] usageEvent;
    public NamedFmodSound[] pickupEvent;
    public NamedFmodSound[] triggerEvents;

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
        if (lastTimeDamage + timeBetweenDamage > Time.time)
            return;

        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        lastTimeDamage = Time.time;

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

        foreach (NamedFmodSound s in usageEvent)
        {
            if (s.name == "" || s.name == item.name)
                FMODUnity.RuntimeManager.PlayOneShot(s.value, transform.position);
        }

    }

    void OnPickup(int interactableID, SOItem item)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        if (pickupEvent == null || pickupEvent.Length <= 0)
            return;

        foreach (NamedFmodSound s in pickupEvent)
        {
            if (s.name == "" || s.name == item.item.itemID)
                FMODUnity.RuntimeManager.PlayOneShot(s.value, transform.position);
        }

    }

    void OnTrigger(int interactableID, string triggerName)
    {
        if (onlyPlayOwnSounds && (entity == null || interactableID != entity.entityID))
            return;

        if (triggerEvents == null || triggerEvents.Length <= 0)
            return;

        foreach (NamedFmodSound s in triggerEvents)
        {
            if (s.name == "" || s.name == triggerName)
                FMODUnity.RuntimeManager.PlayOneShot(s.value, transform.position);
        }

    }
}
