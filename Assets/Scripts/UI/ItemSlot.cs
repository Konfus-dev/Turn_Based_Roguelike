using CodeMonkey.Utils;
using Doozy.Engine.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public enum ItemSlotType
    {
        HelmSlot,
        ChestSlot,
        WeaponSlot,
        ShieldSlot,
        InventorySlot
    }

    public ItemSlotType Type;

    [SerializeField]
    private Transform Player;
    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private Inventory PlayerEquipedItems;
    [SerializeField]
    private Transform ItemContainer;
    [SerializeField]
    private Transform ItemTemplate;

    private void Update()
    {
        CheckIfEquiped();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            Drag_n_Drop drag = rectTrans.GetComponent<Drag_n_Drop>();
            List<Item> equiped = drag.GetEquipedItems().GetItems();
            bool helmAlreadyEquiped = false;
            bool chestAlreadyEquiped = false;
            bool weaponAlreadyEquiped = false;
            bool shieldAlreadyEquiped = false;

            foreach (Item i in equiped)
            {
                if (i.Type.Equals(Item.ItemType.HelmArmor)) helmAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.ChestArmor)) chestAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.Weapon)) weaponAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.Shield)) shieldAlreadyEquiped = true;
            }

            if (Type.Equals(ItemSlotType.HelmSlot) && !helmAlreadyEquiped)
            {
                if(drag.GetItem().Type.Equals(Item.ItemType.HelmArmor))
                {
                    EquipItem(drag);
                }
            }
            else if (Type.Equals(ItemSlotType.ChestSlot) && !chestAlreadyEquiped)
            {
                if (drag.GetItem().Type.Equals(Item.ItemType.ChestArmor))
                {
                    EquipItem(drag);
                }
            }
            else if (Type.Equals(ItemSlotType.WeaponSlot) && !weaponAlreadyEquiped)
            {
                if (drag.GetItem().Type.Equals(Item.ItemType.Weapon))
                {
                    EquipItem(drag);
                }
            }
            else if (Type.Equals(ItemSlotType.ShieldSlot) && !shieldAlreadyEquiped)
            {
                if (drag.GetItem().Type.Equals(Item.ItemType.Shield))
                {
                    EquipItem(drag);
                }
            }
            else
            {
                rectTrans.tag = "InInventory";

                if (! drag.GetInventory().GetItems().Contains(drag.GetItem()) )
                {
                    rectTrans.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                    drag.GetEquipedItems().RemoveItem(drag.GetItem());
                    drag.GetInventory().AddItem(drag.GetItem());
                }
                else
                {
                    drag.SetOrigPos(this.GetComponent<RectTransform>().anchoredPosition);
                }

                rectTrans.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            }

        }
    }

    private void EquipItem(Drag_n_Drop drag)
    {
        Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount };

        Inventory equiped = drag.GetEquipedItems();

        PlayerEquipedItems = equiped;

        equiped.AddItem(itemDup);

        Inventory inv = drag.GetInventory();

        drag.GetInventory().RemoveItem(drag.GetItem());

        RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

        itemRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
            // on right click drop item
            Item itemDup2 = new Item { Type = itemDup.Type, Name = itemDup.Name, Amount = itemDup.Amount };
            equiped.RemoveItem(itemDup);
            ItemInWorld.DropItem(this.Player.transform.position, itemDup2);
            Destroy(itemRectTransform.gameObject);
        };

        Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
        itemIcon.sprite = itemDup.GetSprite();

        itemRectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

        itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
        itemRectTransform.GetComponent<Drag_n_Drop>().SetEquipedItems(equiped);
        itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

        itemRectTransform.gameObject.SetActive(true);

        itemRectTransform.GetChild(0).GetComponent<RectTransform>().sizeDelta = this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta * 5;

        this.Background.SetActive(false);

        itemRectTransform.tag = "Equiped";
    }

    private void CheckIfEquiped()
    {
        if (PlayerEquipedItems != null)
        {
            bool helmAlreadyEquiped = false;
            bool chestAlreadyEquiped = false;
            bool weaponAlreadyEquiped = false;
            bool shieldAlreadyEquiped = false;

            foreach (Item i in PlayerEquipedItems.GetItems())
            {
                if (i.Type.Equals(Item.ItemType.HelmArmor)) helmAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.ChestArmor)) chestAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.Weapon)) weaponAlreadyEquiped = true;
                else if (i.Type.Equals(Item.ItemType.Shield)) shieldAlreadyEquiped = true;
            }

            if (Type.Equals(ItemSlotType.HelmSlot))
            {
                if (!helmAlreadyEquiped) this.Background.SetActive(true);
            }
            else if (Type.Equals(ItemSlotType.ChestSlot))
            {
                if (!chestAlreadyEquiped) this.Background.SetActive(true);
            }
            else if (Type.Equals(ItemSlotType.WeaponSlot))
            {
                if (!weaponAlreadyEquiped) this.Background.SetActive(true);
            }
            else if (Type.Equals(ItemSlotType.ShieldSlot))
            {
                if (!shieldAlreadyEquiped) this.Background.SetActive(true);
            }
        }
    }

}
