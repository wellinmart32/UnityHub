using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryDataManager : MonoBehaviour
{
    private string inventorySaveFile;
    private Inventory inventory;
    private InventoryData inventoryData;
    private bool canLoadData = true;

    private void Awake()
    {
        inventoryData = new InventoryData();
        inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
        inventorySaveFile = Application.persistentDataPath + "/InventoryData.json";
    }

    private void Update()
    {
        if (canLoadData)
        {
            LoadData();
            canLoadData = false;
        }
    }

    public void LoadData()
    {
        if (File.Exists(inventorySaveFile))
        {
            string content = File.ReadAllText(inventorySaveFile);
            inventoryData = JsonUtility.FromJson<InventoryData>(content);
            inventory.UnequipOtherWeapons(-1);
            inventory.CleanSlots();

            for (int i = 0; i < inventory.totalSlots; i++)
            {
                if (!inventoryData.isEmptyList[i])
                {
                    GameObject inventoryItem = inventory.AddItem(inventoryData.itemTypeList[i], inventoryData.subItemTypeList[i]);

                    if (inventoryItem != null)
                    {
                        inventoryItem.GetComponent<Item>().durability = inventoryData.durabilityList[i];
                    }

                }
            }
        }
    }

    public void SaveData()
    {
        int[] idList = new int[inventory.totalSlots];
        Item.ItemType[] itemTypeList = new Item.ItemType[inventory.totalSlots];
        Item.SubItemType[] subItemTypeList = new Item.SubItemType[inventory.totalSlots];
        bool[] isEmptyList = new bool[inventory.totalSlots];
        int[] durabilityList = new int[inventory.totalSlots];
        bool[] isLockList = new bool[inventory.totalSlots];

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();
            idList[i] = slot.id;
        }

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();

            if (slot.item != null)
            {
                itemTypeList[i] = slot.item.GetComponent<Item>().itemType;
            }
        }

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();

            if (slot.item != null)
            {
                subItemTypeList[i] = slot.item.GetComponent<Item>().subItemType;
            }
        }

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();
            isEmptyList[i] = slot.isEmpty;
        }

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();

            if (slot.item != null)
            {
                if (slot.item.GetComponent<Item>() != null)
                {
                    int durability = slot.item.GetComponent<Item>().durability;
                    durabilityList[i] = durability;
                }
            }
        }

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            Slot slot = inventory.slots[i].GetComponent<Slot>();
            isLockList[i] =  slot.isLock;
        }

        InventoryData newInventoryData = new InventoryData()
        {
            idList = idList,
            itemTypeList = itemTypeList,
            subItemTypeList = subItemTypeList,
            isEmptyList = isEmptyList,
            durabilityList = durabilityList,
            isLockList = isLockList,
        };

        string jsonChain = JsonUtility.ToJson(newInventoryData);
        File.WriteAllText(inventorySaveFile, jsonChain);
        Debug.Log("Inventory save successfully");
    }
}
