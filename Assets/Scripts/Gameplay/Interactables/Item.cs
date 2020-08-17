using UnityEngine;

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
}
