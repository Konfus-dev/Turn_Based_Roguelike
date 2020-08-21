using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public enum ItemSlotType
    {
        HelmArmor,
        ChestArmor,
        Weapon,
        Shield,
        Inventory
    }

    public ItemSlotType Type;

    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private Transform ItemContainer;
    [SerializeField]
    private Transform ItemTemplate;

    private Item LastItemEquipped;

    private void Update()
    {
        if (!Type.Equals(ItemSlotType.Inventory)) CheckIfEquiped();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            
            RectTransform rectTrans = eventData.pointerDrag.GetComponent<RectTransform>();
            Drag_n_Drop drag = rectTrans.GetComponent<Drag_n_Drop>();

            if (!Type.Equals(ItemSlotType.Inventory))
            {
                bool itemAlreadyEquipedandMatch = CheckEquippedAndMatch(drag);

                if (itemAlreadyEquipedandMatch)
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
                    drag.GetEquippedItems().RemoveItem(drag.GetItem());
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
        Item itemDup = new Item { Type = drag.GetItem().Type, Name = drag.GetItem().Name, Amount = drag.GetItem().Amount, ArmorMod = drag.GetItem().ArmorMod, DamageMod = drag.GetItem().DamageMod, HealthMod = drag.GetItem().HealthMod };

        LastItemEquipped = itemDup;

        Inventory equipped = drag.GetEquippedItems();

        Player.Instance.SetEquippedItems(equipped);

        equipped.AddItem(itemDup);

        Inventory inv = drag.GetInventory();

        drag.GetInventory().RemoveItem(drag.GetItem());

        RectTransform itemRectTransform = Instantiate(ItemTemplate, ItemContainer).GetComponent<RectTransform>();

        itemRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
            // on right click drop item
            Item itemDup2 = new Item { Type = itemDup.Type, Name = itemDup.Name, Amount = itemDup.Amount, ArmorMod = itemDup.ArmorMod, DamageMod = itemDup.DamageMod, HealthMod = itemDup.HealthMod };
            equipped.RemoveItem(itemDup);
            ItemInWorld.DropItem(Player.Instance.transform.position, itemDup2);
            Destroy(itemRectTransform.gameObject);
        };

        Image itemIcon = itemRectTransform.Find("Icon").gameObject.GetComponent<Image>();
        itemIcon.sprite = itemDup.GetSprite();

        itemRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        itemRectTransform.GetComponent<Drag_n_Drop>().SetInventory(inv);
        itemRectTransform.GetComponent<Drag_n_Drop>().SetEquipedItems(equipped);
        itemRectTransform.GetComponent<Drag_n_Drop>().SetItem(itemDup);

        ApplyItemMods(itemDup);

        itemRectTransform.gameObject.SetActive(true);

        itemRectTransform.GetChild(0).GetComponent<RectTransform>().sizeDelta = transform.GetChild(1).GetComponent<RectTransform>().sizeDelta * 5;

        Background.SetActive(false);

        itemRectTransform.tag = "Equiped";

        
    }

    private void CheckIfEquiped()
    {
        if (Player.Instance.GetEquippedItems() != null)
        {
            bool itemAlreayEquiped = false;

            if(LastItemEquipped != null) itemAlreayEquiped = CheckEquippedAndMatch(null);

            if (itemAlreayEquiped)
            {
                Background.SetActive(true);
                UnApplyItemMods(LastItemEquipped);
                LastItemEquipped = null;
            }
        }
    }

    private bool CheckEquippedAndMatch(Drag_n_Drop drag)
    {
        List<Item> equiped;

        bool itemEquiped = false;
        bool match = false;

        if (drag != null)
        {
            equiped = drag.GetEquippedItems().GetItems();
            match = drag.GetItem().Type.ToString() == this.Type.ToString();
        }
        else equiped = Player.Instance.GetEquippedItems().GetItems();

        foreach (Item i in equiped)
        {
            if (i.Type.ToString() == this.Type.ToString()) itemEquiped = true;
        }

        if (drag != null)
        {
            return !itemEquiped && match;
        }

        return !itemEquiped;
    }

    private void ApplyItemMods(Item item)
    {
        if (item.ArmorMod < 0) Player.Instance.ManageArmor(item.ArmorMod, 0);
        else Player.Instance.ManageArmor(0, item.ArmorMod);

        if (item.HealthMod < 0) Player.Instance.ManageMaxHealth(item.HealthMod, 0);
        else Player.Instance.ManageMaxHealth(0, item.HealthMod);

        if (item.DamageMod < 0) Player.Instance.ManageDamage(item.DamageMod, 0);
        else Player.Instance.ManageDamage(0, item.DamageMod);
    }

    private void UnApplyItemMods(Item item)
    {
        if (item.ArmorMod > 0) Player.Instance.ManageArmor(item.ArmorMod, 0);
        else Player.Instance.ManageArmor(0, item.ArmorMod);

        if (item.HealthMod > 0) Player.Instance.ManageMaxHealth(item.HealthMod, 0);
        else Player.Instance.ManageMaxHealth(0, item.HealthMod);

        if (item.DamageMod > 0) Player.Instance.ManageDamage(item.DamageMod, 0);
        else Player.Instance.ManageDamage(0, item.DamageMod);
    }

}
