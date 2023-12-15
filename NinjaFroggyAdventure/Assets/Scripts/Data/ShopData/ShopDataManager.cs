using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShopDataManager : MonoBehaviour
{
    public int fruits;
    public int briefs;
    public int gems;
    public int lifes;

    public int[] idList;
    public Item.ItemType[] itemTypeList;
    public Item.SubItemType[] subItemTypeList;
    public bool[] isEmptyList;
    public int[] durabilityList;
    public bool[] isLockList;

    public int inventoryLength;
    public bool canSave = true;
    public bool isMerchantShop;


    private string playerSaveFile;
    private string inventorySaveFile;
    private GameData gameData;
    private InventoryData inventoryData;

    private void Awake()
    {
        playerSaveFile = Application.persistentDataPath + "/PlayerGameData.json";
        inventorySaveFile = Application.persistentDataPath + "/InventoryData.json";
        LoadData();
    }

    private void Update()
    {
        if (isMerchantShop)
        {
            MerchantLoadData();
        }
    }

    private void MerchantLoadData()
    {
        PlayerInfo playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;

        fruits = playerInfo.fruits;
        briefs = playerInfo.briefs;
        gems = playerInfo.gems;
        lifes = playerInfo.lifes;
    }

    public bool CanAddItem()
    {
        bool canAddItem = false;

        for (int i = 0; i < idList.Length; i++)
        {
            bool isEmpty = isEmptyList[i];
            bool isLock = isLockList[i];

            if (!isLock && isEmpty)
            {
                canAddItem = true;
                return canAddItem;
            }
        }

        return canAddItem;
    }

    public void LoadData()
    {
        if (File.Exists(playerSaveFile))
        {
            string content = File.ReadAllText(playerSaveFile);
            gameData = JsonUtility.FromJson<GameData>(content);
            fruits = gameData.fruits;
            briefs = gameData.briefs;
            gems = gameData.gems;
            lifes = gameData.lifes;
        }
        else
        {
            fruits = 0;
            briefs = 0;
            gems = 0;
            lifes = 0;
        }

        if (File.Exists(inventorySaveFile))
        {
            string content = File.ReadAllText(inventorySaveFile);
            inventoryData = JsonUtility.FromJson<InventoryData>(content);
            inventoryLength = inventoryData.isEmptyList.Length;
            idList = inventoryData.idList;
            itemTypeList = inventoryData.itemTypeList;
            subItemTypeList = inventoryData.subItemTypeList;
            isEmptyList = inventoryData.isEmptyList;
            durabilityList = inventoryData.durabilityList;
            isLockList = inventoryData.isLockList;
        }
        else
        {
            inventoryLength = 12;       ////
            idList = new int[inventoryLength];
            isEmptyList = new bool[inventoryLength];
            isLockList = new bool[inventoryLength];

            for (int i = 0; i < inventoryLength; i++)
            {
                idList[i] = i;
                isEmptyList[i] = true;

                if (i < 4)
                {
                    isLockList[i] = false;
                }
                else
                {
                    isLockList[i] = true;
                }
            }

            InventoryData newInventoryData = new InventoryData()
            {
                idList = idList,
                itemTypeList = new Item.ItemType[inventoryLength],
                subItemTypeList = new Item.SubItemType[inventoryLength],
                isEmptyList = isEmptyList,
                durabilityList = new int[inventoryLength],
                isLockList = isLockList,
            };

            inventoryData = newInventoryData;

            string jsonChain = JsonUtility.ToJson(newInventoryData);
            File.WriteAllText(inventorySaveFile, jsonChain);

            idList = newInventoryData.idList;
            itemTypeList = newInventoryData.itemTypeList;
            subItemTypeList = newInventoryData.subItemTypeList;
            isEmptyList = newInventoryData.isEmptyList;
            durabilityList = newInventoryData.durabilityList;
            isLockList = newInventoryData.isLockList;
        }
    }

    public void SaveData()
    {
        if (canSave)
        {
            GameData newPlayerData = new GameData()
            {
                levelCompleted = gameData.levelCompleted,
                checkpointReachedLevel = gameData.checkpointReachedLevel,
                startLevel = gameData.startLevel,

                fruits = fruits,
                gems = gems,
                briefs = briefs,
                lifes = lifes,

                currentHearts = gameData.currentHearts,
                currentActiveHearts = gameData.currentActiveHearts,
                maxHearts = gameData.maxHearts,
            };

            string jsonChain = JsonUtility.ToJson(newPlayerData);
            File.WriteAllText(playerSaveFile, jsonChain);

            InventoryData newInventoryData = new InventoryData()
            {
                idList = idList,
                itemTypeList = itemTypeList,
                subItemTypeList = subItemTypeList,
                isEmptyList = isEmptyList,
                durabilityList = durabilityList,
                isLockList = isLockList,
            };

            jsonChain = JsonUtility.ToJson(newInventoryData);
            File.WriteAllText(inventorySaveFile, jsonChain);
        }
    }

    public bool GenerateFileItem(RewardType.Type rewardType, int amount)
    {
        bool didItemCreate = false;

        switch (rewardType)
        {
            case RewardType.Type.BriefsPack:
                briefs += amount;
                return true;
            case RewardType.Type.GemsPack:
                gems += amount;
                return true;
            case RewardType.Type.LifesPack:
                lifes += amount;
                return true;
            case RewardType.Type.RandomCoinReward:
                int num = Random.Range(0, 3);

                if (num == 0)
                {
                    fruits += amount*100;
                }
                else if (num == 1)
                {
                    briefs += amount*5;
                }
                else
                {
                    gems += amount;
                }

                return true;
            default:
                break;
        }

        for (int i = 0; i < inventoryData.isEmptyList.Length; i++)
        {
            bool isEmpty = inventoryData.isEmptyList[i];
            bool isLock = inventoryData.isLockList[i];

            if (!isLock)
            {
                if (isEmpty)
                {
                    inventoryData.isEmptyList[i] = false;

                    switch (rewardType)
                    {
                        case RewardType.Type.BasicSword:
                            inventoryData.itemTypeList[i] = Item.ItemType.Weapon;
                            inventoryData.subItemTypeList[i] = Item.SubItemType.BasicSword;
                            inventoryData.durabilityList[i] = 10;
                            break;
                        case RewardType.Type.LightBall:
                            inventoryData.itemTypeList[i] = Item.ItemType.Weapon;
                            inventoryData.subItemTypeList[i] = Item.SubItemType.LightBall;
                            inventoryData.durabilityList[i] = 3;
                            break;
                        case RewardType.Type.SmallPotion:
                            inventoryData.itemTypeList[i] = Item.ItemType.Potion;
                            inventoryData.subItemTypeList[i] = Item.SubItemType.SmallPotion;
                            inventoryData.durabilityList[i] = 1;
                            break;
                        default:
                            break;
                    }

                    didItemCreate = true;
                    return didItemCreate;
                }
            }
        }

        return didItemCreate;
    }

    public bool GenerateMerchantItem(RewardType.Type elementType, int amount)
    {
        bool didItemCreate = false;
        Inventory inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
        PlayerInfo playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;

        switch (elementType)
        {
            case RewardType.Type.BriefsPack:
                playerInfo.briefs += amount;
                didItemCreate = true;
                break;
            case RewardType.Type.GemsPack:
                playerInfo.gems += amount;
                didItemCreate = true;
                break;
            case RewardType.Type.LifesPack:
                playerInfo.lifes += amount;
                didItemCreate = true;
                break;
            case RewardType.Type.BasicSword:
                if (inventory.AddItem(Item.ItemType.Weapon, Item.SubItemType.BasicSword) != null)
                {
                    didItemCreate = true;
                }
                break;
            case RewardType.Type.LightBall:
                if (inventory.AddItem(Item.ItemType.Weapon, Item.SubItemType.LightBall) != null)
                {
                    didItemCreate = true;
                }
                break;
            case RewardType.Type.SmallPotion:
                if (inventory.AddItem(Item.ItemType.Potion, Item.SubItemType.SmallPotion) != null)
                {
                    didItemCreate = true;
                }
                break;
            default:
                break;
        }

        return didItemCreate;
    }
}
