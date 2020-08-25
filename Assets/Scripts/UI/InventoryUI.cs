using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [HideInInspector]
    public bool draggingItem;

    [HideInInspector]
    public Inventory inventory;

    [SerializeField]
    private Transform slotsContainer;
    [SerializeField]
    private Transform itemTemplate;
    
    private UIManager inventoryManager;

    private void Awake()
    {
        inventoryManager = this.GetComponent<UIManager>();
        CloseInventory();
    }

    public void OpenInventory()
    {
        inventoryManager.ShowItem();
    }

    public void CloseInventory()
    {
        inventoryManager.HideItem();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        this.inventory.onItemListChanged += Inventory_OnItemListChanged;

        Refresh();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        List<Item> itemsInInventory = inventory.GetItems();

        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            Item item = itemsInInventory[i];

            Transform itemSlot = null; 

            foreach (Transform slot in slotsContainer)
            {
                if (slot.childCount == 1 && slot.CompareTag("InventorySlot"))
                {
                    itemSlot = slot;
                    break;
                }
            }

            if (itemSlot == null) continue;

            RectTransform itemRectTransform = Instantiate(itemTemplate, itemSlot).GetComponent<RectTransform>();

            itemRectTransform.anchoredPosition = Vector3.zero;

            itemRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // add left click to use item
                inventory.UseItem(item, null);
            };
            itemRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                // add right click to drop item
                Item itemDup = new Item { type = item.type, itemName = item.itemName, amount = item.amount, armorMod = item.armorMod, damageMod = item.damageMod, healthMod = item.healthMod };
                inventory.RemoveItem(item, itemRectTransform.gameObject, true);
                ItemWorld.DropItem(Player.Instance.transform.position, itemDup);
            };

            Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
            itemIcon.sprite = item.GetSprite();

            Text text = itemRectTransform.Find("Number").GetComponent<Text>();

            if (item.amount > 1)
            {
                text.text = item.amount.ToString();
            }
            else
            {
                text.text = "";
            }

            itemRectTransform.GetComponent<DragItem>().inventory = this.inventory;
            itemRectTransform.GetComponent<DragItem>().item = item;

            itemRectTransform.gameObject.SetActive(true);

        }
    }
}
