using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public Item Item;

    private SpriteRenderer SpriteRenderer;

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item, Transform ItemParent)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity, ItemParent);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private void Awake()
    {
        string ItemName = this.GetComponent<SpriteRenderer>().sprite.name;
        this.Item = new Item { Name = ItemName, Amount = 1 };
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
