using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSkillSelector : MonoBehaviour
{
    public int pos;
    public TextMeshProUGUI info;
    public float waitTime;
    [SerializeField] private string route;
    [SerializeField] private List<SkillInfoTemplate> skillsInfo;
    [SerializeField] private GameObject ActivePlayerSkillTemplate;
    [SerializeField] private GameObject scrollArea;
    private PlayerSkillInfo playerSkillInfo;
    private PlayerSkillDataManager playerSkillDataManager;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        playerSkillInfo = GameObject.Find("DataManager").GetComponent<PlayerSkillInfo>();
        playerSkillDataManager = GameObject.Find("DataManager").GetComponent<PlayerSkillDataManager>();
        SetButtonSounds();
    }

    private void OnEnable()
    {
        var aPST = ActivePlayerSkillTemplate.GetComponent<ActivePlayerSkillTemplate>();

        foreach (var item in skillsInfo)
        {
            if (item.name == "None" || playerSkillDataManager.GetSkillAvailability(item.title))
            {
                aPST.title.text = item.title;
                Instantiate(aPST, scrollArea.transform.GetChild(0).transform);
            }
        }

        HideSkills();
        playerSkillDataManager.LoadData();
    }

    private void OnDisable()
    {
        for (int i = 0; i < scrollArea.transform.GetChild(0).childCount; i++)
        {
            Destroy(scrollArea.transform.GetChild(0).GetChild(i).gameObject);
        }
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

    public void ShowSkills()
    {
        info.gameObject.SetActive(false);
        scrollArea.SetActive(true);
    }

    public void HideSkills()
    {
        info.gameObject.SetActive(true);
        scrollArea.SetActive(false);
    }

    public void SelectPlayerSkill(string skillName)
    {
        buttonClips[0].Play();
        playerSkillInfo.selectedSkillList[pos] = skillName;
        playerSkillDataManager.SaveData();
        HideSkills();
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
