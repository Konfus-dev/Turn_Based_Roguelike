using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDropHandler
{

    public enum SlotType
    {
        HelmArmor,
        ChestArmor,
        FootArmor,
        Weapon
    }

    public SlotType type;

    public Image background;

    private ItemData lastItemEquipped;

    private void Update()
    {
        /*if (Player.Instance.inventories.inventoryUI.draggingItem) this.GetComponent<Image>().raycastTarget = false;
        else this.GetComponent<Image>().raycastTarget = true;*/
        IfNotEquipped();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (this.transform.childCount > 2) return;

            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            ItemUI itemUI = rectTrans.GetComponent<ItemUI>();

            if (itemUI.item.type.ToString() == this.type.ToString())
            {
                EquipItem(itemUI);
            }
        }
    }

    private void EquipItem(ItemUI itemUI)
    {
        lastItemEquipped = itemUI.item.Copy();

        Player.Instance.inventories.equippedItems.AddItem(lastItemEquipped, false);

        itemUI.itemInventory.RemoveItem(itemUI.item, null, false);

        ApplyItemMods(itemUI.item);

        itemUI.itemInventory = Player.Instance.inventories.equippedItems;

        itemUI.GetComponent<ItemUI>().parent = this.transform;

        background.enabled = false;
    }

    private void IfNotEquipped()
    {
        if (this.transform.childCount == 2)
        {
            background.enabled = true;
            if (lastItemEquipped != null)
            {
                UnApplyItemMods(lastItemEquipped);
                lastItemEquipped = null;
            }
        }
        
    }

    private void ApplyItemMods(ItemData item)
    {
        Player.Instance.playerStats.armor += item.armorMod;

        Player.Instance.playerStats.maxHealth += item.healthMod;

        Player.Instance.playerStats.damage += item.damageMod;
    }

    private void UnApplyItemMods(ItemData item)
    {
        Player.Instance.playerStats.armor -= item.armorMod;

        Player.Instance.playerStats.maxHealth -= item.healthMod;

        Player.Instance.playerStats.damage -= item.damageMod;
    }

}
