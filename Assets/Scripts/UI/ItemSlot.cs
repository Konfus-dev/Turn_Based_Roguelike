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
    public Image BackgroundIcon;

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

            if (Type == ItemSlotType.HelmSlot)
            {
                if(drag.GetItem().Type == Item.ItemType.HelmetArmor)
                {
                    dropAllowed = true;
                }
            }
            else if (Type.Equals(ItemSlotType.ChestSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.ChestArmor)
                {
                    dropAllowed = true;
                }
            }
            else if (Type.Equals(ItemSlotType.WeaponSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.Weapon)
                {
                    dropAllowed = true;
                }
            }
            else if (Type.Equals(ItemSlotType.ShieldSlot))
            {
                if (drag.GetItem().Type == Item.ItemType.Shield)
                {
                    dropAllowed = true;
                }
            }
            else
            {
                dropAllowed = true;
            }

            if(dropAllowed)
            {
                //rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x * 1.6f, rectTrans.sizeDelta.y * 1.6f);
                drag.OriginalPos = this.GetComponent<RectTransform>().anchoredPosition;
                //drag.SetSlot(this);
            }

        }
    }

}
