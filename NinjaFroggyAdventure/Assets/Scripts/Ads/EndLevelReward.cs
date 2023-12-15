using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelReward : MonoBehaviour
{
    [SerializeField] private List<AdInfoTemplate> adsInfo;
    [SerializeField] private GameObject adTemplate;
    [SerializeField] private GameObject TemplatesContainer;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        SetButtonSounds();
        InstantiateReward();
    }

    private void InstantiateReward()
    {
        var at = adTemplate.GetComponent<AdTemplate>();
        int num = Random.Range(0, adsInfo.Count);
        var item = adsInfo[num];
        var arb = at.gameObject.GetComponent<AdRewardButton>();
        at.idPos = item.idPos;
        at.image.sprite = item.image;
        at.tittle.text = item.tittle;
        at.amount = item.amount;
        arb.isEndLevelReward = true;
        arb.waitTime = item.waitTime;
        Instantiate(at, TemplatesContainer.transform);
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

    public void Show()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        buttonClips[1].Play();
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(GameObject.Find("EndLevel").GetComponent<EndLevel>().ChangeLevel());
    }
}
