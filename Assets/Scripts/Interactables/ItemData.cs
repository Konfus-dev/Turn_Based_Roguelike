﻿using System;
using UnityEngine;

[Serializable]
public class ItemData 
{
    public enum ItemType
    {
        Weapon,
        HelmArmor,
        ChestArmor,
        Consumable,
        Valuable
    }

    public ItemType type;

    public string itemName;
    public Guid id = Guid.NewGuid();

    public int amount = 1;
    public int maxStackAmount = 1;

    public int manaMod = 0;
    public int damageMod = 0;
    public int healthMod = 0;
    public int armorMod = 0;


    public Sprite GetSprite()
    {
        return ItemDatabase.Instance.SpriteDictionary[itemName];
    }

    public bool IsStackable()
    {
        if(type == ItemType.ChestArmor || type == ItemType.HelmArmor || type == ItemType.Weapon)
        {
            return false;
        }
        else if (type == ItemType.Consumable || type == ItemType.Valuable)
        {
            return true;
        }
        return false;
    }
}