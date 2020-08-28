using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public InventoryUI inventoryUI;

    private Inventory slotInventory;

    private void Start()
    {
        slotInventory = inventoryUI.inventory;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (this.transform.childCount > 1) return;

            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            ItemUI itemUI = rectTrans.GetComponent<ItemUI>();

            ItemData itemDataDup = itemUI.item.Copy();

            itemUI.itemInventory.RemoveItem(itemUI.item, null, false);

            slotInventory.AddItem(itemDataDup, false);

            itemUI.item = itemDataDup;
            itemUI.itemInventory = slotInventory;

            Debug.Log(itemUI.item.itemName + " " + itemUI.item.amount);

            itemUI.GetComponent<ItemUI>().parent = this.transform;

        }
    }

}
