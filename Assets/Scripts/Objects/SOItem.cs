using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class SOItem : ScriptableObject
{
    public Item item;
}

[Serializable]
public class Item
{
    public string itemID;
    public string name;
    public Sprite sprite;
    public int usages;

    public Item (Item other)
    {
        itemID = other.itemID;
        name = other.name;
        sprite = other.sprite;
        usages = other.usages;
    }

    public static bool ContainsAllItems(Dictionary<string, Item> inventory, NamedInt[] requiredItems)
    {

        if (requiredItems != null && requiredItems.Length > 0)
        {
            foreach (NamedInt n in requiredItems)
            {
                if (!inventory.ContainsKey(n.name) || inventory[n.name].usages < n.value)
                    return false;
            }
        }
        return true;
    }

    public static void UseItems(int interactionID, NamedInt[] usedItems)
    {
        foreach (NamedInt n in usedItems)
        {
            GameEvents.current.ItemUsage(interactionID, n);
        }
    }
}