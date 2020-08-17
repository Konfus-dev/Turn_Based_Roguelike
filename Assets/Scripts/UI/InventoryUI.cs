using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory Inventory;
    [SerializeField]
    private Transform ItemSlotContainer;
    [SerializeField]
    private Transform ItemSlotTemplate;
    [SerializeField]
    private Transform ItemContainer;
    [SerializeField]
    private Transform ItemTemplate;

    /*private void Awake()
    {
        ItemsContainer = transform.Find("InventoryContainer");
        ItemSlotTemplate = transform.Find("InvSlot");
    }*/

    public void SetInventory(Inventory inventory)
    {
        this.Inventory = inventory;
        Refresh();
    }

    private void Refresh()
    {
        int x = -150;
        int y = -137;
        int itemSlotCellSize = 75;
        foreach (Item item in Inventory.GetItems())
        {
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);

            RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();
            itemRectTransform.gameObject.SetActive(true);
            itemRectTransform.anchoredPosition = new Vector2(x, y);
            Image itemIcon = itemRectTransform.gameObject.GetComponent<Image>();
            itemIcon.sprite = item.GetSprite();

            x += itemSlotCellSize;

            if (x > Inventory.Size)
            {
                break;
            }
        }
    }
}
