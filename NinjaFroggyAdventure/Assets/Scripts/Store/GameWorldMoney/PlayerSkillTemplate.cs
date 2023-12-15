using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSkillTemplate : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI skillType;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;
    public Button buyButton;
    public bool wasObtained;
    private int remainingCoins;
    private Color disableColor;
    private PlayerSkillDataManager playerSkillDataManager;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        playerSkillDataManager = GameObject.Find("DataManager").GetComponent<PlayerSkillDataManager>();
        disableColor = Color.gray;
        SetButtonSounds();
    }

    private void Start()
    {
        wasObtained = playerSkillDataManager.GetSkillAvailability(title.text);

        if (wasObtained)
        {
            LockButton();
        }
    }

    private void Update()
    {
        if (!wasObtained)
        {
            SetCoins();

            if (int.Parse(price.text) > remainingCoins)
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }

    private void SetButtonSounds()
    {
        buttonClips = new AudioSource[3];
        buttonClips[0] = GameObject.Find("ButtonSoundController").transform.GetChild(0).GetComponent<AudioSource>();
        buttonClips[1] = GameObject.Find("ButtonSoundController").transform.GetChild(1).GetComponent<AudioSource>();
        buttonClips[2] = GameObject.Find("ButtonSoundController").transform.GetChild(2).GetComponent<AudioSource>();
    }

    private void SetCoins()
    {
        remainingCoins = playerSkillDataManager.briefs;
    }

    public void Buy()
    {
        buttonClips[0].Play();
        playerSkillDataManager.briefs -= int.Parse(price.text);
        playerSkillDataManager.BuySkill(title.text);
        LockButton();
        playerSkillDataManager.SaveData();
    }

    public void LockButton()
    {
        buyButton.GetComponent<Image>().color = disableColor;
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Already Got";
        buyButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        buyButton.enabled = false;
    }
}
