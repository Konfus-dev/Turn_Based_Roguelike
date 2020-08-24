using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StorageInventory : MonoBehaviour
{

    public GameObject storageInventory;
    public GameObject playerInventory;
    public GameObject playerEquipment;

    public List<Item> storageItems = new List<Item>();

    [SerializeField]
    private ItemDataBaseList itemDatabase;

    public int distanceToOpenStorage;
    public int itemAmount;

    private Tooltip tooltip;
    private Inventory inv;
    private GameObject player;

    private bool showStorage;

    public void AddItemToStorage(int id, int value)
    {
        Item item = itemDatabase.getItemByID(id);
        item.itemValue = value;
        storageItems.Add(item);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = storageInventory.GetComponent<Inventory>();
        ItemDataBaseList inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        int creatingItemsForChest = 1;

        int randomItemAmount = Random.Range(1, itemAmount);

        while (creatingItemsForChest < randomItemAmount)
        {
            int randomItemNumber = Random.Range(1, inventoryItemList.itemList.Count - 1);
            int raffle = Random.Range(1, 100);

            if (raffle <= inventoryItemList.itemList[randomItemNumber].rarity)
            {
                int randomValue = Random.Range(1, inventoryItemList.itemList[randomItemNumber].getCopy().maxStack);
                Item item = inventoryItemList.itemList[randomItemNumber].getCopy();
                item.itemValue = randomValue;
                storageItems.Add(item);
                creatingItemsForChest++;
            }
        }

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();

    }

    void Update()
    {

        float distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (distance <= distanceToOpenStorage)
        {
            showStorage = !showStorage;
            OpenInventory();
        }

        if (distance > distanceToOpenStorage && showStorage)
        {
            showStorage = false;
            if (storageInventory.activeSelf)
            {
                storageItems.Clear();
                SetListofStorage();
                storageInventory.SetActive(false);
                playerInventory.SetActive(false);
                playerEquipment.SetActive(false);
                inv.DeleteAllItems();
            }
            tooltip.deactivateTooltip();
        }
    }

    public void OpenInventory()
    {
        if (showStorage)
        {
            if (showStorage)
            {
                inv.ItemsInInventory.Clear();
                storageInventory.SetActive(true);
                playerInventory.SetActive(true);
                playerEquipment.SetActive(true);
                AddItemsToInventory();
            }
        }
        else
        {
            storageItems.Clear();
            SetListofStorage();
            storageInventory.SetActive(false);
            playerInventory.SetActive(false);
            playerEquipment.SetActive(false);
            inv.DeleteAllItems();
            tooltip.deactivateTooltip();
        }


    }



    void SetListofStorage()
    {
        Inventory inv = storageInventory.GetComponent<Inventory>();
        storageItems = inv.GetItemList();
    }

    void AddItemsToInventory()
    {
        Inventory iV = storageInventory.GetComponent<Inventory>();
        for (int i = 0; i < storageItems.Count; i++)
        {
            iV.AddItemToInventory(storageItems[i].itemID, storageItems[i].itemValue);
        }
        iV.StackableSettings();
    }






}
