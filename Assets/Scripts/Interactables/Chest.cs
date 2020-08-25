using UnityEngine;

public class Chest : Interactable
{
    public Sprite openChestSprite;
    private InventoryUI chestUI;
    private Inventory chestInventory;

    private void Start()
    {
        chestInventory = new Inventory(null)
        {
            size = 16
        };

            //testing
            chestInventory.AddItem(new ItemData { itemName = "Helmet_1", damageMod = 0, armorMod = 1, healthMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor });
            chestInventory.AddItem(new ItemData { itemName = "Helmet_2", damageMod = 0, armorMod = 2, healthMod = 1, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor });
            chestInventory.AddItem(new ItemData { itemName = "Helmet_3", damageMod = 0, armorMod = 3, healthMod = -1, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor });
            chestInventory.AddItem(new ItemData { itemName = "Items_Consumable_14", damageMod = 0, armorMod = 0, healthMod = 0, amount = 10, maxStackAmount = 10, type = ItemData.ItemType.Consumable });

        chestUI = GameObject.FindGameObjectWithTag("WorldInventory").GetComponent<InventoryUI>();

        chestUI.SetInventory(chestInventory);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OpenChest();
        }
    }

    public override void Interact<T>(T component)
    {
        //Player player = component as Player;

        OpenChest();
    }

    private void OpenChest()
    {
        chestUI.OpenInventory();
        this.GetComponent<SpriteRenderer>().sprite = openChestSprite;
    }

}
