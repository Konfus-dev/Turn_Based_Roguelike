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
    private Transform ItemContainer;
    [SerializeField]
    private Transform ItemTemplate;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            bool dropAllowed = false;

            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            Drag_n_Drop drag = rectTrans.GetComponent<Drag_n_Drop>();

            /*GameObject copy = new GameObject();
            copy = eventData.pointerDrag.gameObject;*/

            if (Type.Equals(ItemSlotType.HelmSlot))
            {
                if(drag.GetItem().Type == Item.ItemType.HelmetArmor)
                {
                    Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount };

                    Inventory inv = drag.GetInventory();

                    drag.GetInventory().RemoveItem(drag.GetItem());

                    RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

                    Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
                    itemIcon.sprite = itemDup.GetSprite();

                    itemRectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

                    itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
                    itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

                    itemRectTransform.gameObject.SetActive(true);

                    itemRectTransform.tag = "Equiped";
                }
            }
            else if (Type.Equals(ItemSlotType.ChestSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.ChestArmor)
                {
                    Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount };

                    Inventory inv = drag.GetInventory();

                    drag.GetInventory().RemoveItem(drag.GetItem());

                    RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

                    Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
                    itemIcon.sprite = itemDup.GetSprite();

                    itemRectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

                    itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
                    itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

                    itemRectTransform.gameObject.SetActive(true);

                    itemRectTransform.tag = "Equiped";
                }
            }
            else if (Type.Equals(ItemSlotType.WeaponSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.Weapon)
                {
                    Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount };

                    Inventory inv = drag.GetInventory();

                    drag.GetInventory().RemoveItem(drag.GetItem());

                    RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

                    Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
                    itemIcon.sprite = itemDup.GetSprite();

                    itemRectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

                    itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
                    itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

                    itemRectTransform.gameObject.SetActive(true);

                    itemRectTransform.tag = "Equiped";
                }
            }
            else if (Type.Equals(ItemSlotType.ShieldSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.Shield)
                {
                    Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount };

                    Inventory inv = drag.GetInventory();

                    drag.GetInventory().RemoveItem(drag.GetItem());

                    RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

                    Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
                    itemIcon.sprite = itemDup.GetSprite();

                    itemRectTransform.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

                    itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
                    itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

                    itemRectTransform.gameObject.SetActive(true);

                    itemRectTransform.tag = "Equiped";
                }
            }
            else
            {
                drag.GetComponent<RectTransform>().tag = "InInventory";
                if (! drag.GetInventory().GetItems().Contains(drag.GetItem()) )
                {
                    drag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                    drag.GetInventory().AddItem(drag.GetItem());
                }
                else
                {
                    drag.OriginalPos = this.GetComponent<RectTransform>().anchoredPosition;
                }
            }

        }
    }

}
