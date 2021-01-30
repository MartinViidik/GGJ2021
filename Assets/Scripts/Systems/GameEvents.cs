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
}
