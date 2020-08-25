using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int size = 5;
    public event EventHandler onItemListChanged;

    private List<ItemData> itemsData;
    private Action<ItemData, GameObject> useItemAction;

    public Inventory(Action<ItemData, GameObject> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemsData = new List<ItemData>();
    }

    public void AddItem(ItemData item)
    {
        if (itemsData.Contains(item)) itemsData.Remove(item);

        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach (ItemData inventoryItem in itemsData)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    if (inventoryItem.amount == item.maxStackAmount) break;
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                itemsData.Add(item);
            }
        }
        else
        {
            itemsData.Add(item);
        }

        onItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(ItemData item, GameObject itemGameObj, bool destroy)
    {
        if (itemsData.Count == 0) return;

        if (item.IsStackable())
        {
            ItemData itemInInventory = null;
            foreach (ItemData inventoryItem in itemsData)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemsData.Remove(itemInInventory);
                if(itemGameObj != null) Destroy(itemGameObj);
            }
        }
        else
        {
            itemsData.Remove(item);
            if (destroy && itemGameObj != null) Destroy(itemGameObj);
        }

        if (item.type == ItemData.ItemType.Consumable) onItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(ItemData item, GameObject itemGameObj)
    {
        useItemAction(item, itemGameObj);
    }

    public List<ItemData> GetItems()
    {
        return itemsData;
    }
}
