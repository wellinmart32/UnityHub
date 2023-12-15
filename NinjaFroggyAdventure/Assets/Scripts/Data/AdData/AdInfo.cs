using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdInfo : MonoBehaviour
{
    public string[] idList;
    public string[] nameList;
    public DateTime[] endDateList;
    public RewardType.Type[] rewardTypeList;
    public int[] amountList;
    private ShopDataManager shopDataManager;
    private AdDataManager adDataManager;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    private void Awake()
    {
        shopDataManager = GameObject.Find("DataManager").GetComponent<ShopDataManager>();
        adDataManager = GameObject.Find("DataManager").GetComponent<AdDataManager>();
    }

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => {
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }

    public string GetId(int idPos)
    {
        string id = "";

        for (int i = 0; i < idList.Length; i++)
        {
            if (i == idPos)
            {
                id = idList[i];
                return id;
            }
        }

        return id;
    }

    private void SetEndDate(int idPos, DateTime endDate)
    {
        for (int i = 0; i < idList.Length; i++)
        {
            if (i == idPos)
            {
                endDateList[i] = endDate;
            }
        }
    }

    public DateTime GetEndDate(int idPos)
    {
        DateTime endDate;

        for (int i = 0; i < idList.Length; i++)
        {
            if (i == idPos)
            {
                endDate = endDateList[i];
                return endDate;
            }
        }

        return new DateTime(2000, 1, 1, 1, 1, 1, 1);
    }

    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(idList[1], adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    return;
                }

                _interstitialAd = ad;
            });
    }

    public void LoadRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(idList[0], adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    return;
                }

                _rewardedAd = ad;
            });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
            RegisterEventHandlers(_interstitialAd);
            RegisterReloadHandler(_interstitialAd);
        }
    }

    public void ShowRewardedAd(int idPos, DateTime endDate, bool isEndLevelReward, AdRewardButton adRewardButton)
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            SetEndDate(idPos, endDate);
            _rewardedAd.Show((Reward reward) => {});
            RegisterEventHandlers(_rewardedAd);
            RegisterReloadHandler(_rewardedAd, idPos, isEndLevelReward, adRewardButton);
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {

        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {

        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {

        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd();
        };
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {

        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {

        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {

        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }

    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

    private void RegisterReloadHandler(RewardedAd ad, int idPos, bool isEndLevelReward, AdRewardButton adRewardButton)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();

            if (idPos >= 0 && idPos < idList.Length)
            {
                if (isEndLevelReward)
                {
                    GenerateEndLevelReward(idPos);
                    adRewardButton.StartCount();
                    GameObject.Find("EndLevelReward").GetComponent<EndLevelReward>().NextLevel();
                }
                else
                {
                    if (shopDataManager.GenerateFileItem(rewardTypeList[idPos], amountList[idPos]))
                    {
                        adRewardButton.StartCount();
                        shopDataManager.SaveData();
                        adDataManager.SaveData();
                    }
                }
            }
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    private void GenerateEndLevelReward(int idPos)
    {
        Inventory inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
        PlayerInfo playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;

        RewardType.Type rewardType = rewardTypeList[idPos];
        int amount = amountList[idPos];

        switch (rewardType)
        {
            case RewardType.Type.BriefsPack:
                playerInfo.briefs += amount;
                break;
            case RewardType.Type.GemsPack:
                playerInfo.gems += amount;
                break;
            case RewardType.Type.LifesPack:
                playerInfo.lifes += amount;
                break;
            case RewardType.Type.BasicSword:
                inventory.AddItem(Item.ItemType.Weapon, Item.SubItemType.BasicSword);
                break;
            case RewardType.Type.LightBall:
                inventory.AddItem(Item.ItemType.Weapon, Item.SubItemType.LightBall);
                break;
            case RewardType.Type.SmallPotion:
                inventory.AddItem(Item.ItemType.Potion, Item.SubItemType.SmallPotion);
                break;
            case RewardType.Type.RandomCoinReward:
                int num = UnityEngine.Random.Range(0, 3);

                if (num == 0)
                {
                    playerInfo.fruits += amount * 100;
                }
                else if (num == 1)
                {
                    playerInfo.briefs += amount * 5;
                }
                else
                {
                    playerInfo.gems += amount;
                }
                break;
            default:
                break;
        }
    }
}
