using UnityEngine;
using System.Collections;
public class PickUpItem : MonoBehaviour
{
    public Item item;
    private Inventory inventory;

    private void Awake()
    {
        ItemDataBaseList inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
        this.item = inventoryItemList.getItemByName(this.name);
    }

    private void Start()
    {
        inventory = Player.Instance.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool check = inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
            if (check)
                return;
            else if (inventory.ItemsInInventory.Count < (inventory.width * inventory.height))
            {
                inventory.addItemToInventory(item.itemID, item.itemValue);
                inventory.updateItemList();
                inventory.stackableSettings();
                Destroy(this.gameObject);
            }
        }
    }

}