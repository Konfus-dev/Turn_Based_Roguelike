using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            DragItem drag = rectTrans.GetComponent<DragItem>();

            ItemData itemDataDup = new ItemData 
            { 
                id = drag.item.id, 
                type = drag.item.type,
                itemName = drag.item.itemName, 
                amount = drag.item.amount, 
                armorMod = drag.item.armorMod, 
                damageMod = drag.item.damageMod, 
                healthMod = drag.item.healthMod, 
                manaMod = drag.item.manaMod 
            };

            drag.inventory.RemoveItem(drag.item, null, false);

            slotInventory.AddItem(itemDataDup, false);

            drag.item = itemDataDup;
            drag.inventory = slotInventory;

            Debug.Log(drag.item.itemName + " " + drag.item.amount);

            drag.GetComponent<DragItem>().parent = this.transform;

        }
    }

}
