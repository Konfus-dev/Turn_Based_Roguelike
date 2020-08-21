using System;
using UnityEngine;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Weapon,
        HelmetArmor,
        ChestArmor,
        Shield,
        Consumable,
        Valuable
    }
    [HideInInspector]
    public ItemType Type;
    [HideInInspector]
    public string Name;
    public int Amount;

    public Sprite GetSprite()
    {
        return ItemAssets.Instance.SpriteDictionary[Name];
    }

    public bool IsStackable()
    {
        if(Type == ItemType.ChestArmor || Type == ItemType.HelmetArmor || Type == ItemType.Shield || Type == ItemType.Weapon)
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
