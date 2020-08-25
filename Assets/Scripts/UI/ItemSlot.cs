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
            Debug.Log("Drop on inv slot");

            if (this.transform.childCount > 1) return;

            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            DragItem drag = rectTrans.GetComponent<DragItem>();

            rectTrans.tag = "InInventory";

            if (drag.inventory != slotInventory)
            {
                drag.inventory.RemoveItem(drag.item, null, false);
                slotInventory.AddItem(drag.item);
            }

            rectTrans.GetComponent<DragItem>().parent = this.transform;

        }
    }

}
