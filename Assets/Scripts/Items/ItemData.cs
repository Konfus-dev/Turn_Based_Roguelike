using System;
using UnityEngine;

[Serializable]
public class ItemData 
{
    public enum ItemType
    {
        Weapon,
        HelmArmor,
        ChestArmor,
        FootArmor,
        Consumable,
        Valuable
    }

    public ItemType type;

    //[HideInInspector]
    public Guid id = Guid.NewGuid();
    //[HideInInspector]
    public string itemName;

    [TextArea(5,10)]
    public string description;

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

    public ItemData Copy()
    {
        return new ItemData
        {
            type = this.type,
            id = this.id,
            itemName = this.itemName,
            amount = this.amount,
            maxStackAmount = this.maxStackAmount,
            manaMod = this.manaMod,
            damageMod = this.damageMod,
            healthMod = this.healthMod,
            armorMod = this.armorMod
        };
    }
}
