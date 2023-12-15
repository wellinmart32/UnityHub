using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Template/ItemInfoShop")]
public class ItemInfoTemplate : ScriptableObject
{
    public string tittle;
    public Sprite image;
    public Sprite[] coinsImage;
    public float price;
    public int amount;
    public enum CoinType { Fruits, Briefs, Gems, Dollar };
    public CoinType coinType;
    public RewardType.Type rewardType;
}

