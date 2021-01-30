using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public GameObject itemDisplayPrefab;
    public Transform displayHolder;
    List<UIItemDisplay> itemDisplays = new List<UIItemDisplay>();

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerInventoryUpdate += InventoryUpdate;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerInventoryUpdate -= InventoryUpdate;
    }

    
    void InventoryUpdate(Dictionary<string, Item> inventory)
    {
        for (int i = itemDisplays.Count - 1; i >= 0; i--)
            Destroy(itemDisplays[i].gameObject);

        itemDisplays.Clear();

        foreach (Item item in inventory.Values)
        {
            UIItemDisplay temp = Instantiate(itemDisplayPrefab).GetComponent<UIItemDisplay>();
            temp.UpdateDisplay(item);
            itemDisplays.Add(temp);
            temp.transform.SetParent(displayHolder);
        }
    }
}
