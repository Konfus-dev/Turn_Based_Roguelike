using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [HideInInspector]
    public Inventory equippedItems;
    [HideInInspector]
    public Inventory inventory;

    public InventoryUI inventoryUI;

    private void Start()
    {
        inventory = new Inventory(UseItem)
        {
            size = 5
        };

        inventoryUI.SetInventory(inventory);

            //testing
            inventory.AddItem(new ItemData { itemName = "Sword_1", damageMod = 1, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_2", damageMod = 2, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Helmet_3", damageMod = 3, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Items_Consumable_14", damageMod = 0, armorMod = 0, healthMod = 1, manaMod = 0, amount = 10, maxStackAmount = 10, type = ItemData.ItemType.Consumable });
            inventory.AddItem(new ItemData { itemName = "Sword_5", damageMod = 5, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });

        equippedItems = new Inventory(null)
        {
            size = 4
        };
    }

    private void Update()
    {
        CheckOpenCloseInventory();
        
    }

    public void UseItem(ItemData item, GameObject itemGameObj)
    {
        if (item.type == ItemData.ItemType.Consumable) 
        {
            item.amount--;

            if(item.amount <= 0)
            {
                inventory.RemoveItem(item, itemGameObj);
            }

            ApplyItemModsOnUse(item);

            Text text = itemGameObj.GetComponent<RectTransform>().Find("Number").GetComponent<Text>();

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

    private void CheckOpenCloseInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!(Player.Instance.CurrentState == Player.PlayerState.InMenu))
            {
                inventoryUI.OpenInventory();
                Player.Instance.SetState(Player.PlayerState.InMenu); 
            }
            else
            {
                inventoryUI.CloseInventory();
                Player.Instance.SetState(Player.PlayerState.NotMoving);
                Player.Instance.Check();
            }
        }
    }
}
