using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    public AudioSource clip;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.05f;
    public bool inBox;
    public bool inChest;
    private GameObject itemManager;
    private Inventory inventory;

    private void Awake()
    {
        Item item = GetComponent<Item>();

        if (item)
        {
            if (item.itemType == Item.ItemType.Weapon)
            {
                clip = GameObject.Find("ItemPickUp").GetComponent<AudioSource>();
            }
            else
            {
                clip = GameObject.Find("ItemPickUp (1)").GetComponent<AudioSource>();
            }
           
        }

        itemManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().itemManager;
        inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();

    }

    private void Update()
    {
        if (canPickUp)
        {
            Item item = gameObject.GetComponent<Item>();
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            canPickUp = false;
            inventory.AddItem(item.itemType, item.subItemType);
            StartCoroutine(DisableItem());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inventory.CheckAvalibleSlot())
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                canPickUp = true;
                HideItem();
                clip.Play();
            }
        }
    }

    public void RestoreItem()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        isCollected = false;
        transform.SetParent(itemManager.transform);
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    public IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}
