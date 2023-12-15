using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static IAPPurchase;

public class ShopItemTemplate : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI amountText;
    public Image coinImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI tittle;
    public Button buyButton;
    public ItemInfoTemplate.CoinType coinType;
    public RewardType.Type rewardType;
    public bool isUniqueBuy;
    public int amount;
    public int identifier;
    private float price;
    private int remainingCoins;
    private ShopDataManager shopDataManager;
    public IAPPurchase iAPPurchase;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        price = float.Parse(priceText.text);
        amountText.text = "X" + amount.ToString();
        shopDataManager = GameObject.Find("DataManager").GetComponent<ShopDataManager>();

        if (coinType == ItemInfoTemplate.CoinType.Gems)
        {
            coinImage.rectTransform.sizeDelta = new Vector2(50, 50);
        }

        if (GameObject.Find("ItemStore") != null)
        {
            iAPPurchase = GameObject.Find("ItemStore").GetComponent<IAPPurchase>();
        }

        SetCoins();
        SetButtonSounds();
    }

    private void Update()
    {
        SetCoins();

        if (price > remainingCoins && coinType != ItemInfoTemplate.CoinType.Dollar)
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    private void SetButtonSounds()
    {
        buttonClips = new AudioSource[3];
        buttonClips[0] = GameObject.Find("ButtonSoundController").transform.GetChild(0).GetComponent<AudioSource>();
        buttonClips[1] = GameObject.Find("ButtonSoundController").transform.GetChild(1).GetComponent<AudioSource>();
        buttonClips[2] = GameObject.Find("ButtonSoundController").transform.GetChild(2).GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventPurchaser.onBuyConsumableState += BuyConsumable;
    }

    private void OnDisable()
    {
        EventPurchaser.onBuyConsumableState -= BuyConsumable;
    }

    private void SetCoins()
    {
        switch (coinType)
        {
            case ItemInfoTemplate.CoinType.Fruits:
                remainingCoins = shopDataManager.fruits;
                break;
            case ItemInfoTemplate.CoinType.Briefs:
                remainingCoins = shopDataManager.briefs;
                break;
            case ItemInfoTemplate.CoinType.Gems:
                remainingCoins = shopDataManager.gems;
                break;
            case ItemInfoTemplate.CoinType.Dollar:
                break;
            default:
                remainingCoins = shopDataManager.fruits;
                break;
        }
    }

    public void Buy()
    {
        if (coinType == ItemInfoTemplate.CoinType.Dollar)
        {
            iAPPurchase.purchaseIdentifier = identifier;
            iAPPurchase.Buy10GemsPack();
            return;
        }

        if (shopDataManager.GenerateFileItem(rewardType, amount))
        {
            buttonClips[0].Play();

            switch (coinType)
            {
                case ItemInfoTemplate.CoinType.Fruits:
                    shopDataManager.fruits -= (int)price;
                    break;
                case ItemInfoTemplate.CoinType.Briefs:
                    shopDataManager.briefs -= (int)price;
                    break;
                case ItemInfoTemplate.CoinType.Gems:
                    shopDataManager.gems -= (int)price;
                    break;
                default:
                    shopDataManager.fruits -= (int)price;
                    break;
            }

            shopDataManager.SaveData();
        }
        else
        {
            buttonClips[2].Play();
        }
    }

    public void BuyConsumable(bool state, int purchaseIdentifier)
    {
        if (state && identifier == purchaseIdentifier)
        {
            buttonClips[0].Play();
            shopDataManager.GenerateFileItem(rewardType, amount);
            shopDataManager.SaveData();
            iAPPurchase.purchaseIdentifier = -1;
        }
    }

    public void MerchantBuy()
    {
        if (shopDataManager.GenerateMerchantItem(rewardType, amount))
        {
            buttonClips[0].Play();
            PlayerInfo playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;

            switch (coinType)
            {
                case ItemInfoTemplate.CoinType.Fruits:
                    playerInfo.fruits -= (int)price;
                    break;
                case ItemInfoTemplate.CoinType.Briefs:
                    playerInfo.briefs -= (int)price;
                    break;
                case ItemInfoTemplate.CoinType.Gems:
                    playerInfo.gems -= (int)price;
                    break;
                default:
                    playerInfo.fruits -= (int)price;
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
