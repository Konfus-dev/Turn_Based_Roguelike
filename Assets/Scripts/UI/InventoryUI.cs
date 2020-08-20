using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform ItemSlotContainer;
    [SerializeField]
    private Transform ItemSlotTemplate;
    [SerializeField]
    private Transform ItemContainer;
    [SerializeField]
    private Transform ItemTemplate;
    [SerializeField]
    private UIManager InventoryManager;

    private Inventory Inventory;
    private Player Player;

    private void Awake()
    {
        /*ItemContainer = transform.Find("InventoryContainer");
        ItemSlotTemplate = transform.Find("InvSlot");*/
        this.CloseInventory();
    }

    public void SetPlayer(Player player)
    {
        this.Player = player;
    }

    public void OpenInventory()
    {
        InventoryManager.ShowItem();
    }

    public void CloseInventory()
    {
        InventoryManager.HideItem();
    }

    public void SetInventory(Inventory inventory)
    {
        this.Inventory = inventory;

        Inventory.OnItemListChanged += Inventory_OnItemListChanged;

        Refresh();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        /*foreach (Transform itemSlot in ItemSlotContainer)
        {
            if (itemSlot != ItemSlotTemplate && itemSlot.name != "Background" && itemSlot.name != "Border") Destroy(itemSlot.gameObject);
        }*/
        foreach (Transform item in ItemContainer)
        {
            if (item != ItemTemplate) Destroy(item.gameObject);
        }

        int x = -150;
        int y = -137;
        int itemSlotCellSize = 75;

        foreach (Item item in Inventory.GetItems())
        {
            //Debug.Log(item.Name);
           /* RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);*/
            
            RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

            itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(this.Inventory);
            itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(item);

            itemRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // use item
            };
            itemRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // drop item
                Item itemDup = new Item { Type = item.Type, Name = item.Name, Amount = item.Amount };
                Inventory.RemoveItem(item);
                ItemInWorld.DropItem(this.Player.transform.position, itemDup);
            };

            itemRectTransform.gameObject.SetActive(true);
            itemRectTransform.sizeDelta = new Vector2(ItemSlotTemplate.GetComponent<RectTransform>().sizeDelta.x * 1.6f, 
                ItemSlotTemplate.GetComponent<RectTransform>().sizeDelta.y * 1.6f);
            itemRectTransform.anchoredPosition = new Vector2(x, y);
            Image itemIcon = itemRectTransform.gameObject.GetComponent<Image>();
            itemIcon.sprite = item.GetSprite();

            Text text = itemRectTransform.Find("Number").GetComponent<Text>();

            if (item.Amount > 1)
            {
                text.text = item.Amount.ToString();
            }
            else
            {
                text.text = "";
            }

            x += itemSlotCellSize;

        }
    }
}
