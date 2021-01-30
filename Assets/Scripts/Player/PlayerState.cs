using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateHandler
{
    public int maxHealth = 5;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

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
        Invoke("SetupPlayer", 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDealDamage -= OnDamage;
    }

    void SetupPlayer()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CurrentHealth -= 1;

        if (Input.GetKeyDown(KeyCode.E))
            CurrentHealth += 1;
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
}
