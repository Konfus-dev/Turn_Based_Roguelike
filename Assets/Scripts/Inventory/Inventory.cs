using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public int size = 5;
    public event EventHandler onItemListChanged;

    private List<Item> items;
    private Action<Item, GameObject> useItemAction;

    public Inventory(Action<Item, GameObject> useItemAction)
    {
        this.useItemAction = useItemAction;
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (items.Contains(item)) items.Remove(item);

        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in items)
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
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }

        onItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item, GameObject itemGameObj, bool destroy)
    {
        if (items.Count == 0) return;

        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                items.Remove(itemInInventory);
                if(itemGameObj != null) Destroy(itemGameObj);
            }
        }
        else
        {
            items.Remove(item);
            if (destroy && itemGameObj != null) Destroy(itemGameObj);
        }

        if (item.type == Item.ItemType.Consumable) onItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item, GameObject itemGameObj)
    {
        useItemAction(item, itemGameObj);
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
