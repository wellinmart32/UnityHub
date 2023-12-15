using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public bool isActive;
    public bool isMerchantShop;
    public float waitTime;
    public GameObject mobileControls;
    public GameObject optionsButton;
    [SerializeField] private string route;
    [SerializeField] private TextMeshProUGUI totalFruitsText;
    [SerializeField] private TextMeshProUGUI totalBriefsText;
    [SerializeField] private TextMeshProUGUI totalGemsText;
    [SerializeField] private TextMeshProUGUI totalLifesText;
    [SerializeField] private List<ItemInfoTemplate> itemsInfo;
    [SerializeField] private List<AdInfoTemplate> adsInfo;
    [SerializeField] private GameObject shopItemTemplate;
    [SerializeField] private GameObject adTemplate;
    [SerializeField] private GameObject TemplatesContainer;
    private ShopDataManager shopDataManager;
    private TimeController timeController;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        shopDataManager = GameObject.Find("DataManager").GetComponent<ShopDataManager>();

        if (isMerchantShop)
        {
            timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
        }

        SetButtonSounds();
    }

    private void Start()
    {
        int count = 0;
        totalFruitsText.text = shopDataManager.fruits.ToString();
        totalBriefsText.text = shopDataManager.briefs.ToString();
        totalGemsText.text = shopDataManager.gems.ToString();
        totalLifesText.text = shopDataManager.lifes.ToString();
        var sit = shopItemTemplate.GetComponent<ShopItemTemplate>();
        var at = adTemplate.GetComponent<AdTemplate>();

        foreach (var item in itemsInfo)
        {
            sit.identifier = count;
            sit.image.sprite = item.image;

            switch (item.coinType)
            {
                case ItemInfoTemplate.CoinType.Fruits:
                    sit.coinImage.sprite = item.coinsImage[0];
                    break;
                case ItemInfoTemplate.CoinType.Briefs:
                    sit.coinImage.sprite = item.coinsImage[1];
                    break;
                case ItemInfoTemplate.CoinType.Gems:
                    sit.coinImage.sprite = item.coinsImage[2];
                    break;
                case ItemInfoTemplate.CoinType.Dollar:
                    sit.coinImage.sprite = item.coinsImage[3];
                    break;
                default:
                    break;
            }

            sit.tittle.text = item.tittle;
            sit.priceText.text = item.price.ToString();
            sit.coinType = item.coinType;
            sit.rewardType = item.rewardType;
            sit.amount = item.amount;
            Instantiate(sit, TemplatesContainer.transform);
            count++;
        }

        foreach (var item in adsInfo)
        {
            var arb = at.gameObject.GetComponent<AdRewardButton>();
            at.idPos = item.idPos;
            at.image.sprite = item.image;
            at.tittle.text = item.tittle;
            at.amount = item.amount;
            arb.isEndLevelReward = false;
            arb.waitTime = item.waitTime;
            Instantiate(at, TemplatesContainer.transform);
        }
    }

    private void Update()
    {
        totalFruitsText.text = shopDataManager.fruits.ToString();
        totalBriefsText.text = shopDataManager.briefs.ToString();
        totalGemsText.text = shopDataManager.gems.ToString();
        totalLifesText.text = shopDataManager.lifes.ToString();
    }
    private void OnEnable()
    {
        shopDataManager.LoadData();
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

    public void OpenMainMenu()
    {
        StartCoroutine(OpenRoute(route));
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator OpenRoute(string route)
    {
        buttonClips[1].Play();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(route);
    }
}
