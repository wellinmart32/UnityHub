using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AdRewardButton : MonoBehaviour
{
    public Button rewardButton;
    public TextMeshProUGUI remainTimeText;
    public GameObject videoImage;
    public int idPos;
    public int waitTime;
    public DateTime currentDate;
    public DateTime endDate;
    public bool isEndLevelReward;
    private bool canCount;
    private Color enableColor;
    private Color disableColor;
    private ShopDataManager shopDataManager;
    private AdInfo adInfo;
    private Inventory inventory;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        if (!isEndLevelReward)
        {
            shopDataManager = GameObject.Find("DataManager").GetComponent<ShopDataManager>();
        }
        else
        {
            inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
        }

        if (GetComponent<AdTemplate>())
        {
            idPos = GetComponent<AdTemplate>().idPos;
        }

        adInfo = GameObject.Find("DataManager").GetComponent<AdInfo>();
        enableColor = rewardButton.GetComponent<Image>().color;
        disableColor = Color.gray;
        remainTimeText.gameObject.SetActive(false);
        SetButtonSounds();
    }

    private void Start()
    {
        currentDate = DateTime.Now;
        endDate = adInfo.GetEndDate(idPos);

        if (endDate > currentDate)
        {
            LockButton();
            canCount = true;
        }
    }

    private void Update()
    {
        if (canCount)
        {
            CalculateTime();
        }
        else
        {
            UpdateButtonVisibility();
        }
    }

    private void SetButtonSounds()
    {
        buttonClips = new AudioSource[3];
        buttonClips[0] = GameObject.Find("ButtonSoundController").transform.GetChild(0).GetComponent<AudioSource>();
        buttonClips[1] = GameObject.Find("ButtonSoundController").transform.GetChild(1).GetComponent<AudioSource>();
        buttonClips[2] = GameObject.Find("ButtonSoundController").transform.GetChild(2).GetComponent<AudioSource>();
    }

    public void UpdateButtonVisibility()
    {
        RewardType.Type rewardType = adInfo.rewardTypeList[idPos];

        if (isEndLevelReward)
        {
            if (!inventory.CanAddItem())
            {
                if (rewardType == RewardType.Type.BasicSword || rewardType == RewardType.Type.LightBall
                    || rewardType == RewardType.Type.SmallPotion)
                {
                    DisableButton();
                }
            }
        }
        else
        {
            if (!shopDataManager.CanAddItem())
            {
                if (rewardType == RewardType.Type.BasicSword || rewardType == RewardType.Type.LightBall
                    || rewardType == RewardType.Type.SmallPotion)
                {
                    DisableButton();
                }
            }
        }
    }

    public void StartCount()
    {
        LockButton();
        canCount = true;
    }

    private void CalculateTime()
    {

        currentDate = DateTime.Now;
        TimeSpan remainTime = endDate - currentDate;
        remainTimeText.text = remainTime.Minutes.ToString() + ":" + remainTime.Seconds.ToString().PadLeft(2, '0');

        if (currentDate >= endDate)
        {
            UnlockButton();
            canCount = false;
        }
    }

    public void GetReward()
    {
        buttonClips[1].Play();
        currentDate = DateTime.Now;
        endDate = currentDate.AddSeconds(waitTime);
        adInfo.ShowRewardedAd(idPos, endDate, isEndLevelReward, this);
    }

    public void DisableButton()
    {
        rewardButton.GetComponent<Button>().interactable = false;
    }

    public void LockButton()
    {
        rewardButton.GetComponent<Image>().color = disableColor;
        rewardButton.enabled = false;
        videoImage.SetActive(false);
        remainTimeText.gameObject.SetActive(true);
    }

    public void UnlockButton()
    {
        rewardButton.GetComponent<Button>().interactable = true;
        rewardButton.GetComponent<Image>().color = enableColor;
        rewardButton.enabled = true;
        videoImage.SetActive(true);
        remainTimeText.gameObject.SetActive(false);
    }
}
