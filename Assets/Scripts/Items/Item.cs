using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    
    public ItemData itemData;
    
    private SpriteRenderer spriteRenderer;
    private TMP_Text text;

    private void Awake()
    {

        text = transform.GetChild(0).transform.Find("Text").GetComponent<TMP_Text>();
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;

        if (this.itemData != null)
        {
            itemData = new ItemData { itemName = ItemName, description = itemData.description, amount = this.itemData.amount, damageMod = this.itemData.damageMod, healthMod = this.itemData.healthMod, armorMod = this.itemData.armorMod };

            if (CompareTag("Weapon|Shield")) itemData.type = ItemData.ItemType.Weapon;
            else if (CompareTag("ChestArmor")) itemData.type = ItemData.ItemType.ChestArmor;
            else if (CompareTag("HelmArmor")) itemData.type = ItemData.ItemType.HelmArmor;
            else if (CompareTag("FootArmor")) itemData.type = ItemData.ItemType.FootArmor;
            else if (CompareTag("Consumable")) itemData.type = ItemData.ItemType.Consumable;
            else if (CompareTag("Valuable")) itemData.type = ItemData.ItemType.Valuable;

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
        itemInWorld.SetItem(item);
        itemInWorld.spriteRenderer = ItemWorldTemplate.GetComponent<SpriteRenderer>();
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
        this.itemData = item.Copy();

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
