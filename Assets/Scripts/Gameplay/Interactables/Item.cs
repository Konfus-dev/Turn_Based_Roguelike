using System;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Valuable
    }

    public ItemType Type;
    public string Name;
    public int Amount;

    public Sprite GetSprite()
    {
        return ItemAssets.Instance.SpriteDictionary[Name];
    }

    public bool IsStackable()
    {
        if(Type == ItemType.Armor || Type == ItemType.Weapon)
        {
            return false;
        }
        else if (Type == ItemType.Consumable || Type == ItemType.Valuable)
        {
            return true;
        }
        return true;
    }
}
