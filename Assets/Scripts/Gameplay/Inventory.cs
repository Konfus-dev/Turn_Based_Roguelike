using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public int Size = 6;
    public event EventHandler OnItemListChanged;
    private List<Item> Items;

    public Inventory()
    {
        Items = new List<Item>();

        AddItem(new Item { Name = "Sword_6", Amount = 1 });
        AddItem(new Item { Name = "Helmet_3", Amount = 1 });
        AddItem(new Item { Name = "Items_Consumable_14", Amount = 1 });
    }

    public void AddItem(Item item)
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        Items.Add(item);
    }

    public List<Item> GetItems()
    {
        return Items;
    }
}
