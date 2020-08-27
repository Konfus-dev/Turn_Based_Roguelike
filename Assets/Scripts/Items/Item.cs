using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    
    public ItemData itemData;
    
    private SpriteRenderer spriteRenderer;
    private Text text;

    private void Awake()
    {

        this.text = transform.GetChild(0).transform.Find("Text").GetComponent<Text>();
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;

        if (this.itemData != null)
        {

            if (this.tag == "Weapon|Shield") this.itemData = new ItemData { type = ItemData.ItemType.Weapon, itemName = ItemName, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };
            else if (this.tag == "ChestArmor") this.itemData = new ItemData { type = ItemData.ItemType.ChestArmor, itemName = ItemName, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };
            else if (this.tag == "HelmArmor") this.itemData = new ItemData { type = ItemData.ItemType.HelmArmor, itemName = ItemName, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };
            else if (this.tag == "Consumable") this.itemData = new ItemData { type = ItemData.ItemType.Consumable, itemName = ItemName, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };
            else if (this.tag == "Valuable") this.itemData = new ItemData { type = ItemData.ItemType.Valuable, itemName = ItemName, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };

            if (this.itemData.amount > 1)
            {
                this.text.text = this.itemData.amount.ToString();
            }
            else
            {
                this.text.text = "";
            }

        }

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public static Item SpawnItemInWorld(Vector3 position, ItemData item)
    {
        Transform itemParent = GameObject.Find("Interactables").transform;

        Transform ItemWorldTemplate = Instantiate(ItemDatabase.Instance.ItemWorldTemplate, position, Quaternion.identity, itemParent);

        Item itemInWorld = ItemWorldTemplate.gameObject.AddComponent<Item>();

        itemInWorld.gameObject.SetActive(true);
        itemInWorld.spriteRenderer = ItemWorldTemplate.GetComponent<SpriteRenderer>();
        itemInWorld.SetItem(item);
        itemInWorld.spriteRenderer.sprite = item.GetSprite();

        return itemInWorld;
    }

    public static Item DropItem(Vector3 dropPos, ItemData item)
    {
        return SpawnItemInWorld(dropPos + Vector3.down, item);
    }

    public void SetItem(ItemData item)
    {
        this.tag = item.type.ToString();
        this.itemData = item;
        spriteRenderer.sprite = this.itemData.GetSprite();

        if(this.itemData.amount > 1)
        {
            this.text.text = this.itemData.amount.ToString();
        }
        else
        {
            this.text.text = "";
        }
        
    }

    public ItemData GetItem()
    {
        return itemData;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
