using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public int Size = 6;

    private List<Item> Items;

    public Inventory()
    {
        Items = new List<Item>();

        AddItem(new Item { Type = Item.ItemType.Weapon, Name = "Sword_6", Amount = 1 });
        AddItem(new Item { Type = Item.ItemType.Armor, Name = "Helmet_3", Amount = 1 });
        AddItem(new Item { Type = Item.ItemType.Consumable, Name = "Items_Consumable_14", Amount = 1 });
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public List<Item> GetItems()
    {
        return Items;
    }
}
