using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int id;
    public enum ItemType { Weapon, Potion };
    public ItemType itemType;

    public enum SubItemType { BasicSword, LightBall, SmallPotion, MediumPotion };
    public SubItemType subItemType;

    public string description;
    public Sprite icon;

    public int durability;

    [HideInInspector]
    public bool pickUp;

    [HideInInspector]
    public bool isEquipped;

    [HideInInspector]
    public GameObject weaponManager;

    [HideInInspector]
    public GameObject weapon;

    public bool isPlayerWeapon;
    private Inventory inventory;

    public AudioSource clip;
    public float disableTime = 0.05f;

    public void Awake()
    {
        weaponManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().weaponManager;
        inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();

        if (itemType == ItemType.Potion)
        {
            clip = GameObject.Find("UsePotion").GetComponent<AudioSource>();
        }
        else
        {
            clip = GameObject.Find("EquipWeapon").GetComponent<AudioSource>();
        }
        
        InitItemData();
    }

    public void InitItemData()
    {
        if (!isPlayerWeapon)
        {
            int totalWeapons = weaponManager.transform.childCount;

            for (int i = 0; i < totalWeapons; i++)
            {
                if (itemType == ItemType.Weapon)
                {
                    if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().subItemType == subItemType)
                    {
                        weapon = weaponManager.transform.GetChild(i).gameObject;
                        return;
                    }
                }
            }
        }
    }

    public void ItemUsage(Slot slot)
    {
        if (itemType == ItemType.Weapon)
        {
            weapon.GetComponent<Item>().isEquipped = !weapon.GetComponent<Item>().isEquipped;
            isEquipped = !isEquipped;

            if (isEquipped)
            {
                clip.Play();
                slot.GetComponent<Image>().color = Color.red;
                inventory.UnequipOtherWeapons(id);
                weapon.SetActive(true);

                switch (subItemType)
                {
                    case SubItemType.BasicSword:
                        weapon.GetComponentInChildren<Sword2D>().item = this;
                        break;
                    case SubItemType.LightBall:
                        weapon.GetComponentInChildren<Ball>().item = this;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                slot.GetComponent<Image>().color = new Color(255, 255,255, 0.39f);
                weapon.SetActive(false);
            }
        }
        else if (itemType == ItemType.Potion)
        {
            GameObject player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
            clip.time = 0.3f;
            clip.Play();

            switch (subItemType)
            {
                case SubItemType.SmallPotion:
                    player.GetComponent<RecoveryPlayer>().RecoveryByPotion(2);
                    break;
                case SubItemType.MediumPotion:
                    player.GetComponent<RecoveryPlayer>().RecoveryByPotion(5);
                    break;
                default:
                    break;
            }

            DecreaseDurability(slot);
        }
    }

    public void DecreaseDurability(Slot slot)
    {
        durability--;
        slot.DurabilityText.text = "X" + durability.ToString();

        if (durability <= 0)
        {
            slot.EmptySlot();

            if (itemType == ItemType.Weapon)
            {
                switch (subItemType)
                {
                    case SubItemType.BasicSword:
                        weapon.GetComponentInChildren<Sword2D>().item = null;
                        break;
                    case SubItemType.LightBall:
                        weapon.GetComponentInChildren<Ball>().item = null;
                        break;
                    default:
                        break;
                }

                weapon.gameObject.SetActive(false);
            }

            //StartCoroutine(DisableItem());
            Invoke("DisableItem", disableTime);
        }
    }

    public void DisableItem()
    {
        //yield return new WaitForSeconds(disableTime);
        Destroy(gameObject);
    }
}
