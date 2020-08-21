using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInWorld : MonoBehaviour
{
    
    public Item Item;
    
    private SpriteRenderer SpriteRenderer;
    private Text Text;

    private void Awake()
    {

        this.Text = transform.GetChild(0).transform.Find("Text").GetComponent<Text>();
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;

        if (this.Item != null)
        {

            if (this.tag == "Weapon") this.Item = new Item { Type = Item.ItemType.Weapon, Name = ItemName, Amount = this.Item.Amount };
            else if (this.tag == "Shield") this.Item = new Item { Type = Item.ItemType.Shield, Name = ItemName, Amount = this.Item.Amount };
            else if (this.tag == "ChestArmor") this.Item = new Item { Type = Item.ItemType.ChestArmor, Name = ItemName, Amount = this.Item.Amount };
            else if (this.tag == "HelmArmor") this.Item = new Item { Type = Item.ItemType.HelmetArmor, Name = ItemName, Amount = this.Item.Amount };
            else if (this.tag == "Consumable") this.Item = new Item { Type = Item.ItemType.Consumable, Name = ItemName, Amount = this.Item.Amount };
            else if (this.tag == "Valuable") this.Item = new Item { Type = Item.ItemType.Valuable, Name = ItemName, Amount = this.Item.Amount };

            if (this.Item.Amount > 1)
            {
                this.Text.text = this.Item.Amount.ToString();
            }
            else
            {
                this.Text.text = "";
            }

        }

        this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public static ItemInWorld SpawnItemInWorld(Vector3 position, Item item)
    {
        Transform itemParent = GameObject.Find("Interactables").transform;

        Transform ItemWorldTemplate = Instantiate(ItemAssets.Instance.ItemWorldTemplate, position, Quaternion.identity, itemParent);

        ItemInWorld itemInWorld = ItemWorldTemplate.gameObject.AddComponent<ItemInWorld>();

        itemInWorld.gameObject.SetActive(true);
        itemInWorld.SpriteRenderer = ItemWorldTemplate.GetComponent<SpriteRenderer>();
        itemInWorld.SetItem(item);
        itemInWorld.SpriteRenderer.sprite = item.GetSprite();

        return itemInWorld;
    }

    public static ItemInWorld DropItem(Vector3 dropPos, Item item)
    {
        return SpawnItemInWorld(dropPos + Vector3.down, item);
    }

    public void SetItem(Item item)
    {
        this.tag = item.Type.ToString();
        this.Item = item;
        SpriteRenderer.sprite = Item.GetSprite();

        if(Item.Amount > 1)
        {
            this.Text.text = Item.Amount.ToString();
        }
        else
        {
            this.Text.text = "";
        }
        
    }

    public Item GetItem()
    {
        return Item;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
