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
        inventory = new Inventory()
        {
            size = 5
        };

        inventoryUI.SetInventory(inventory);

            //testing
            /*inventory.AddItem(new ItemData { itemName = "Sword_1", description = "sword 1", damageMod = 1, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon }, false);
            inventory.AddItem(new ItemData { itemName = "Sword_2", description = "sword 2", damageMod = 2, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.Weapon }, false);
            inventory.AddItem(new ItemData { itemName = "Helmet_3", description = "helm 3", damageMod = 3, armorMod = 0, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor }, false);
            inventory.AddItem(new ItemData { itemName = "Items_Consumable_14", description = "health potion", damageMod = 0, armorMod = 0, healthMod = 1, manaMod = 0, amount = 10, maxStackAmount = 10, type = ItemData.ItemType.Consumable }, true);
            */
        equippedItems = new Inventory()
        {
            size = 4
        };
    }

    private void Update()
    {
        CheckOpenCloseInventory();
        
    }

    private void CheckOpenCloseInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!(Player.Instance.CurrentState == Player.PlayerState.InMenu))
            {
                inventoryUI.Refresh();
                inventoryUI.OpenInventory();
                Player.Instance.SetState(Player.PlayerState.InMenu, true); 
            }
            else
            {
                inventoryUI.CloseInventory();
                Player.Instance.SetState(Player.PlayerState.Idle);
                Player.Instance.Check();
            }
        }
    }
}
