using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableInterfaceSlots : MonoBehaviour
{
    public int availableSlots;
    public int totalSlots;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
    }

    public void SetAvailableSlots()
    {
        for (int i = 0; i < totalSlots; i++)
        {
            inventory.slots[i].SetActive(false);
            inventory.slots[i].GetComponent<Slot>().isLock = true;
        }

        for (int i = 0; i < availableSlots; i++)
        {
            inventory.slots[i].SetActive(true);
            inventory.slots[i].GetComponent<Slot>().isLock = false;
        }

        //if (x1AS % 2 == 0)
        //{
        //    temp1 = x1AS / 2;
        //    temp2 = x1AS / 2;
        //}
        //else
        //{
        //    temp1 = x1AS / 2;
        //    temp2 = x1AS / 2 + 1;
        //}

        //for (int i = x1; i <= x1 + temp1; i++)
        //{
        //    Debug.Log(inventory.slots[i].name);
        //    inventory.slots[i].SetActive(true);
        //}

        //for (int i = x1; i >= x1 - temp2; i--)
        //{
        //    inventory.slots[i].SetActive(true);
        //}

        //if (x2AS % 2 == 0)
        //{
        //    temp1 = x2AS / 2;
        //    temp2 = x2AS / 2;
        //}
        //else
        //{
        //    temp1 = x2AS / 2;
        //    temp2 = x2AS / 2 - 1;
        //}

        //for (int i = x2; i <= x2 + temp1; i++)
        //{
        //    inventory.slots[i].SetActive(true);
        //}

        //for (int i = x2; i >= x2 - temp2; i--)
        //{
        //    inventory.slots[i].SetActive(true);
        //}
    }
}
