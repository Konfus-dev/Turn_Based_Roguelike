using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UseItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ConsumeItem(this.GetComponent<DragItem>().item, this.gameObject, this.GetComponent<DragItem>().inventory);
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            DragItem drag = this.GetComponent<DragItem>();

            if (this.transform.parent.GetComponent<ItemSlot>().inventoryUI.gameObject.CompareTag("WorldInventory"))
            {
                if (Player.Instance.inventories.inventory.GetItems().Count == Player.Instance.inventories.inventory.size) return;

                drag.inventory.RemoveItem(drag.item, null, false);
                Player.Instance.inventories.inventory.AddItem(drag.item, false);

                Transform itemSlot = null;

                foreach (Transform slot in Player.Instance.inventories.inventoryUI.slotsContainer)
                {
                    if (slot.CompareTag("InventorySlot"))
                    {
                        if (slot.childCount == 1)
                        {
                            itemSlot = slot;
                            break;
                        }
                    }
                }

                Debug.Log("Item in world inv");

                this.GetComponent<RectTransform>().parent = itemSlot;
                this.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            }
            else
            {
                Debug.Log("Item in player inv");
                ItemData itemDup = new ItemData 
                { 
                    type = drag.item.type, 
                    itemName = drag.item.itemName, 
                    amount = drag.item.amount, 
                    armorMod = drag.item.armorMod, 
                    damageMod = drag.item.damageMod, 
                    healthMod = drag.item.healthMod 
                };

                drag.inventory.RemoveItem(drag.item, this.GetComponent<RectTransform>().gameObject, true);
                Item.DropItem(Player.Instance.transform.position, itemDup);
            }
        }

    }

    public void ConsumeItem(ItemData item, GameObject itemGameObj, Inventory itemInv)
    {
        if (item.type == ItemData.ItemType.Consumable)
        {
            ItemData itemDataDup = new ItemData
            {
                id = item.id,
                type = item.type,
                itemName = item.itemName,
                amount = 1,
                armorMod = item.armorMod,
                damageMod = item.damageMod,
                healthMod = item.healthMod,
                manaMod = item.manaMod
            };

            ApplyItemModsOnUse(itemDataDup);

            itemInv.RemoveItem(itemDataDup, null, false);

            TMP_Text text = itemGameObj.GetComponent<RectTransform>().Find("Number").GetComponent<TMP_Text>();

            if (item.amount > 1)
            {
                text.text = item.amount.ToString();
            }
            else
            {
                text.text = "";
            }
        }
    }

    private void ApplyItemModsOnUse(ItemData item)
    {
        Player.Instance.playerStats.currentHealth += item.healthMod;
        Player.Instance.playerStats.currentMana += item.manaMod;
    }
}
