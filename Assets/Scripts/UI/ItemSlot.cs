using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private InventoryUI inventoryUI;

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

            if(drag.inventory != slotInventory)
            {
                drag.inventory.RemoveItem(drag.item, drag.gameObject);

                slotInventory.AddItem(drag.item);

                drag.inventory = slotInventory;
            }

            drag.GetComponent<DragItem>().parent = this.transform;

        }
    }

}
