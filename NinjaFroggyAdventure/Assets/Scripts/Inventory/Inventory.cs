using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotHolder;
    public GameObject[] slots;
    public int totalSlots;
    public GameObject[] itemList;
    public bool isActive;
    public bool canShow = true;
    public GameObject mobileControls;
    public GameObject optionsButton;
    private TimeController timeController;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
        totalSlots = slotHolder.transform.childCount;
        slots = new GameObject[totalSlots];
        transform.GetChild(0).gameObject.SetActive(true);
        SetButtonSounds();
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        CleanSlots();
        GetComponent<AvailableInterfaceSlots>().SetAvailableSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchVisibility();
        }
    }

    public void SetButtonSounds()
    {
        if (GameObject.Find("ButtonSoundController"))
        {
            buttonClips = new AudioSource[3];
            buttonClips[0] = GameObject.Find("ButtonSoundController").transform.GetChild(0).GetComponent<AudioSource>();
            buttonClips[1] = GameObject.Find("ButtonSoundController").transform.GetChild(1).GetComponent<AudioSource>();
            buttonClips[2] = GameObject.Find("ButtonSoundController").transform.GetChild(2).GetComponent<AudioSource>();
        }
    }

    public void SwitchVisibility()
    {
        buttonClips[2].Play();

        if (canShow)
        {
            isActive = !isActive;

            if (isActive)
            {
                timeController.StopTime();
                transform.GetChild(0).gameObject.SetActive(true);
                optionsButton.SetActive(false);
                mobileControls.SetActive(false);
            }
            else
            {
                timeController.RestoreTime();
                transform.GetChild(0).gameObject.SetActive(false);
                optionsButton.SetActive(true);
                mobileControls.SetActive(true);
            }
        }
    }

    public bool CanAddItem()
    {
        bool canAddItem = false;

        for (int i = 0; i < totalSlots; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();

            if (!slot.isLock && slot.isEmpty)
            {
                canAddItem = true;
                return canAddItem;
            }
        }

        return canAddItem;
    }

    public void CleanSlots()
    {
        for(int i = 0; i < totalSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
            slots[i].GetComponent<Slot>().id = i;
            slots[i].GetComponent<Slot>().EmptySlot();
        }
    }

    public GameObject AddItem(Item.ItemType itemType, Item.SubItemType subItemType)
    {
        for (int i = 0; i < totalSlots; i++)
        {
            if (!slots[i].GetComponent<Slot>().isLock)
            {
                if (slots[i].GetComponent<Slot>().isEmpty)
                {
                    GameObject inventoryItem = GenerateInventoryItem(slots[i], itemType, subItemType);
                    inventoryItem.GetComponent<Item>().pickUp = true;
                    inventoryItem.GetComponent<Item>().id = slots[i].GetComponent<Slot>().id;

                    slots[i].GetComponent<Slot>().item = inventoryItem;
                    slots[i].GetComponent<Slot>().icon = inventoryItem.GetComponent<Item>().icon;
                    slots[i].GetComponent<Slot>().UpdateSlot();
                    slots[i].GetComponent<Slot>().isEmpty = false;
                    return inventoryItem;
                }
            }
        }

        return null;
    }

    public GameObject GenerateInventoryItem(GameObject slot, Item.ItemType itemType, Item.SubItemType subItemType)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            Item item = itemList[i].GetComponent<Item>();

            if (item.itemType == itemType && item.subItemType == subItemType)
            {
                GameObject inventoryItem = Instantiate(itemList[i], new Vector2(0, 0), new Quaternion());
                inventoryItem.transform.SetParent(slot.transform);
                return inventoryItem;
            }
        }

        return null;
    }

    public void UnequipOtherWeapons(int id)
    {
        for (int i = 0; i < totalSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().item != null)
            {
                Item item = slots[i].GetComponent<Slot>().item.GetComponent<Item>();

                if (item.id != id && item.isEquipped)
                {
                    item.isEquipped = false;
                    item.weapon.SetActive(false);
                    slots[i].GetComponent<Image>().color = new Color(255, 255, 255, 0.39f);
                }
            }
        }
    }

    public bool CheckAvalibleSlot()
    {
        bool canPickUp = false;

        for (int i = 0; i < totalSlots; i++)
        {
            if (!slots[i].GetComponent<Slot>().isLock)
            {
                if (slots[i].GetComponent<Slot>().isEmpty)
                {
                    canPickUp = true;
                    break;
                }
            }
        }

        return canPickUp;
    }
}
