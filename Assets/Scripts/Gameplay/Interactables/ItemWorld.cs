using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item, Transform ItemParent)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity, ItemParent);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item Item;
    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.Item = item;
        SpriteRenderer.sprite = Item.GetSprite();
    }
}
