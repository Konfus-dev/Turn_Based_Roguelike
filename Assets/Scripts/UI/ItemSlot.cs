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

    private void Update()
    {
        /*if (Player.Instance.inventories.inventoryUI.draggingItem) this.GetComponent<Image>().raycastTarget = false;
        else this.GetComponent<Image>().raycastTarget = true;*/
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (this.transform.childCount > 1) return;

            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            DragItem drag = rectTrans.GetComponent<DragItem>();

            drag.inventory.RemoveItemNoUpdate(drag.item, null, false);

            slotInventory.AddItemNoUpdate(drag.item);

            drag.inventory = slotInventory;

            drag.GetComponent<DragItem>().parent = this.transform;

        }
    }

}
