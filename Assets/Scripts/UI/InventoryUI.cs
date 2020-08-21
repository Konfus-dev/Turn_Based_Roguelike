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
    private Inventory EquipedItems;
    private Player Player;

    private void Awake()
    {
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

    public void SetEquipedItems(Inventory equiped)
    {
        this.EquipedItems = equiped;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        Dictionary<Item, int> occupiedSlots = new Dictionary<Item, int>();

        foreach (Transform item in ItemContainer)
        {
            if(item.GetComponent<Drag_n_Drop>().GetItem() != null)
            {
                if (occupiedSlots.ContainsKey(item.GetComponent<Drag_n_Drop>().GetItem()))
                    occupiedSlots.Remove(item.GetComponent<Drag_n_Drop>().GetItem());
                occupiedSlots.Add(item.GetComponent<Drag_n_Drop>().GetItem(), (int)item.GetComponent<Drag_n_Drop>().GetComponent<RectTransform>().anchoredPosition.x);
            }
            if (item != ItemTemplate && !item.CompareTag("Equiped")) Destroy(item.gameObject);
        }

        int x = -150;
        int y = -137;
        int itemSlotCellSize = 75;

        foreach (Item item in Inventory.GetItems())
        {
            RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

            itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(this.Inventory);
            itemRectTransform.GetComponent<Drag_n_Drop>().SetEquipedItems(this.EquipedItems);
            itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(item);

            itemRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // on left click use item
                Inventory.UseItem(item);
            };
            itemRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                // on right click drop item
                occupiedSlots.Remove(item);
                Item itemDup = new Item { Type = item.Type, Name = item.Name, Amount = item.Amount };
                Inventory.RemoveItem(item);
                ItemInWorld.DropItem(this.Player.transform.position, itemDup);
            };

            if(item != null && occupiedSlots.Count > 0 && occupiedSlots.ContainsKey(item))
            {
                itemRectTransform.anchoredPosition = new Vector2(occupiedSlots[item], y);
            }
            else
            {

                while (occupiedSlots.ContainsValue(x))
                {
                    x += itemSlotCellSize;
                }

                if (x > 150) x = 0;

                itemRectTransform.anchoredPosition = new Vector2(x, y);
            }

            Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
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

            itemRectTransform.gameObject.SetActive(true);

        }
    }
}
