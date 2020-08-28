using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    public int size;

    private List<ItemData> itemsData;

    public Inventory()
    {
        itemsData = new List<ItemData>();
    }

    public bool AddItem(ItemData item, bool update)
    {
        //if (itemsData.Contains(item)) itemsData.Remove(item);

        bool destroyItem = true;

        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach (ItemData inventoryItem in itemsData)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    if (inventoryItem.amount == item.maxStackAmount)
                    {
                        destroyItem = false;
                        break;
                    }
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

        if (update) OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return destroyItem;
    }
    

    public void RemoveItem(ItemData item, GameObject itemGameObj, bool update)
    {
        if (item.IsStackable())
        {
            ItemData itemInInventory = null;
            foreach (ItemData inventoryItem in itemsData)
            {
                if (inventoryItem.id == item.id)
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
            if (itemGameObj != null) Destroy(itemGameObj);
        }

        if (update) OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemData> GetItems()
    {
        return itemsData;
    }
}
