using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{

    public Item Item;

    private SpriteRenderer SpriteRenderer;

    public static ItemInWorld SpawnItemInWorld(Vector3 position, Item item, Transform ItemParent)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorldTemplate, position, Quaternion.identity, ItemParent);

        ItemInWorld itemWorld = transform.GetComponent<ItemInWorld>();
        
        itemWorld.SetItem(item);
        itemWorld.SpriteRenderer.sprite = item.GetSprite();
        itemWorld.gameObject.SetActive(true);

        return itemWorld;
    }

    private void Start()
    {
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;

        if (this.tag == "Weapon") this.Item = new Item { Type = Item.ItemType.Weapon, Name = ItemName, Amount = 1 };
        else if (this.tag == "Armor") this.Item = new Item { Type = Item.ItemType.Armor, Name = ItemName, Amount = 1 };
        else if (this.tag == "Consumable") this.Item = new Item { Type = Item.ItemType.Consumable, Name = ItemName, Amount = 1 };
        else if (this.tag == "Valuable") this.Item = new Item { Type = Item.ItemType.Valuable, Name = ItemName, Amount = 1 };
        
        this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.Item = item;
        SpriteRenderer.sprite = Item.GetSprite();
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
