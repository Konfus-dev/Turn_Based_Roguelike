using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [HideInInspector]
    public bool draggingItem;

    [HideInInspector]
    public Inventory inventory;

    public Transform slotsContainer;

    [SerializeField]
    private Transform itemTemplate;
    
    private UIManager inventoryManager;

    private void Awake()
    {
        inventoryManager = this.GetComponent<UIManager>();
        CloseInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string items = "";
            foreach (ItemData i in this.inventory.GetItems()) 
            {
                items += i.itemName + ", ";
            }
            Debug.Log(this.tag + ": " + items);
        }
    }

    public void OpenInventory()
    {
        inventoryManager.ShowItem();
    }

    public void CloseInventory()
    {
        this.GetComponent<Doozy.Engine.UI.UIView>().RectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        inventoryManager.HideItem();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        this.inventory.OnItemListChanged += Inventory_OnItemListChanged;

        Refresh();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        List<ItemData> itemsInInventory = inventory.GetItems();

        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            ItemData item = itemsInInventory[i];

            Transform itemSlot = null; 

            foreach (Transform slot in slotsContainer)
            {
                if(slot.CompareTag("InventorySlot"))
                {
                    if (slot.childCount == 1)
                    {
                        itemSlot = slot;
                        break;
                    }
                    else if (slot.GetChild(1).GetComponent<DragItem>().item.id == item.id)
                    {
                        if (i == itemsInInventory.Count - 1) return;
                        i++;
                        item = itemsInInventory[i];
                    }
                }
            }

            if (itemSlot == null)
            {
                continue;
            }

            RectTransform itemRectTransform = Instantiate(itemTemplate, itemSlot).GetComponent<RectTransform>();

            itemRectTransform.GetComponent<DragItem>().parent = itemSlot;
            itemRectTransform.GetComponent<DragItem>().inventory = this.inventory;
            itemRectTransform.GetComponent<DragItem>().item = item;

            itemRectTransform.anchoredPosition = Vector3.zero;

            Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
            itemIcon.sprite = item.GetSprite();

            TMP_Text text = itemRectTransform.Find("Number").GetComponent<TMP_Text>();

            if (item.amount > 1)
            {
                text.text = item.amount.ToString();
            }
            else
            {
                text.text = "";
            }

            itemRectTransform.gameObject.SetActive(true);

        }
    }
}
