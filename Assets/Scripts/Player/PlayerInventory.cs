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
            inventory.AddItem(new ItemData { itemName = "Sword_1", damageMod = 1, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_2", damageMod = 2, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_3", damageMod = 3, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
            inventory.AddItem(new ItemData { itemName = "Sword_4", damageMod = 4, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon });
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
            ApplyItemModsOnUse(item);

            itemGameObj.GetComponent<DragItem>().inventory.RemoveItemNoUpdate(new ItemData { id = item.id, type = item.type, itemName = item.itemName, amount = 1, armorMod = item.armorMod, damageMod = item.damageMod, healthMod = item.healthMod,manaMod = item.manaMod }, itemGameObj, true);
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
