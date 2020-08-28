using UnityEngine;

public class Chest : ReactiveEntity
{
    public Sprite openChestSprite;
    private InventoryUI chestUI;
    private Inventory chestInventory;

    
    private void Start()
    {
        chestInventory = new Inventory()
        {
            size = 16
        };

            //testing
            chestInventory.AddItem(new ItemData { itemName = "Helmet_1", description = "helm  1", damageMod = 0, armorMod = 1, healthMod = 0, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor }, true);
            chestInventory.AddItem(new ItemData { itemName = "Helmet_2", description = "helm 2", damageMod = 0, armorMod = 2, healthMod = 1, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor }, true);
            chestInventory.AddItem(new ItemData { itemName = "Helmet_3", description = "helm 3", damageMod = 0, armorMod = 3, healthMod = -1, manaMod = 0, amount = 1, maxStackAmount = 1, type = ItemData.ItemType.HelmArmor }, true);
            chestInventory.AddItem(new ItemData { itemName = "Items_Consumable_14", description = "health potion", damageMod = 0, armorMod = 0, healthMod = 1, manaMod = 0, amount = 10, maxStackAmount = 10, type = ItemData.ItemType.Consumable }, true);

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
