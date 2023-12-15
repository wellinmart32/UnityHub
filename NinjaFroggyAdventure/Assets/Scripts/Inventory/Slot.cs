using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public bool isEmpty;
    public Sprite icon;
    public Transform slotIcon;
    public TextMeshProUGUI DurabilityText;
    public GameObject item;
    public float destroyTime = 0;
    public bool isLock = true;
    private Sprite emptySlotIcon;

    private void Awake()
    {
        slotIcon = transform.GetChild(0);
        emptySlotIcon = slotIcon.GetComponent<Image>().sprite;
        EmptySlot();
    }

    public void UpdateSlot()
    {
        slotIcon.GetComponent<Image>().sprite = icon;
        DurabilityText.enabled = true;
        DurabilityText.text = "X" + item.GetComponent<Item>().durability.ToString();
    }

    public void UseItem()
    {
        if (!isEmpty)
        {
            item.GetComponent<Item>().ItemUsage(this);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }

    public void EmptySlot()
    {
        item = null;
        isEmpty = true;
        icon = null;
        slotIcon.GetComponent<Image>().sprite = emptySlotIcon;
        DurabilityText.text = "X0";
        DurabilityText.enabled = false;
        GetComponent<Image>().color = new Color(255, 255, 255, 0.39f);
        
        if (transform.childCount > 1)
        {
            Invoke("DestroyItem", destroyTime);
        }
    }

    public void DestroyItem()
    {
        if (transform.childCount >= 2)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
    }
}
