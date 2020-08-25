using UnityEngine;
using UnityEngine.UI;

public class ItemWorld : MonoBehaviour
{
    
    public Item item;
    
    private SpriteRenderer spriteRenderer;
    private Text text;

    private void Awake()
    {

        this.text = transform.GetChild(0).transform.Find("Text").GetComponent<Text>();
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;

        if (this.item != null)
        {

            if (this.tag == "Weapon|Shield") this.item = new Item { type = Item.ItemType.Weapon, itemName = ItemName, amount = this.item.amount, damageMod = this.item.damageMod, healthMod = this.item.healthMod, armorMod = this.item.armorMod };
            else if (this.tag == "ChestArmor") this.item = new Item { type = Item.ItemType.ChestArmor, itemName = ItemName, amount = this.item.amount, damageMod = this.item.damageMod, healthMod = this.item.healthMod, armorMod = this.item.armorMod };
            else if (this.tag == "HelmArmor") this.item = new Item { type = Item.ItemType.HelmArmor, itemName = ItemName, amount = this.item.amount, damageMod = this.item.damageMod, healthMod = this.item.healthMod, armorMod = this.item.armorMod };
            else if (this.tag == "Consumable") this.item = new Item { type = Item.ItemType.Consumable, itemName = ItemName, amount = this.item.amount, damageMod = this.item.damageMod, healthMod = this.item.healthMod, armorMod = this.item.armorMod };
            else if (this.tag == "Valuable") this.item = new Item { type = Item.ItemType.Valuable, itemName = ItemName, amount = this.item.amount, damageMod = this.item.damageMod, healthMod = this.item.healthMod, armorMod = this.item.armorMod };

            if (this.item.amount > 1)
            {
                this.text.text = this.item.amount.ToString();
            }
            else
            {
                this.text.text = "";
            }

        }

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public static ItemWorld SpawnItemInWorld(Vector3 position, Item item)
    {
        Transform itemParent = GameObject.Find("Interactables").transform;

        Transform ItemWorldTemplate = Instantiate(ItemDatabase.Instance.ItemWorldTemplate, position, Quaternion.identity, itemParent);

        ItemWorld itemInWorld = ItemWorldTemplate.gameObject.AddComponent<ItemWorld>();

        itemInWorld.gameObject.SetActive(true);
        itemInWorld.spriteRenderer = ItemWorldTemplate.GetComponent<SpriteRenderer>();
        itemInWorld.SetItem(item);
        itemInWorld.spriteRenderer.sprite = item.GetSprite();

        return itemInWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPos, Item item)
    {
        return SpawnItemInWorld(dropPos + Vector3.down, item);
    }

    public void SetItem(Item item)
    {
        this.tag = item.type.ToString();
        this.item = item;
        spriteRenderer.sprite = this.item.GetSprite();

        if(this.item.amount > 1)
        {
            this.text.text = this.item.amount.ToString();
        }
        else
        {
            this.text.text = "";
        }
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
