using UnityEngine;

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
            inventory.AddItem(new ItemData { itemName = "Sword_1", damageMod = 1, armorMod = 0, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_2", damageMod = 2, armorMod = 0, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_3", damageMod = 3, armorMod = 0, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_4", damageMod = 4, armorMod = 0, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_5", damageMod = 5, armorMod = 0, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });

        equippedItems = new Inventory(null)
        {
            size = 4
        };

    }

    private void Update()
    {
        CheckOpenCloseInventory();
    }

    private void UseItem(ItemData item, GameObject itemGameObj)
    {
        if (item.type == ItemData.ItemType.Consumable)
        {
            inventory.RemoveItem(new ItemData { type = item.type, itemName = item.itemName, amount = 1, armorMod = item.armorMod, damageMod = item.damageMod, healthMod = item.healthMod }, itemGameObj, false);
        }
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
