using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillsStore : MonoBehaviour
{
    public float waitTime;
    public List<SkillInfoTemplate> skillsInfo;
    public TextMeshProUGUI totalBriefsText;
    [SerializeField] private string route;
    [SerializeField] private GameObject playerSkillTemplate;
    [SerializeField] private GameObject TemplatesContainer;
    private PlayerSkillDataManager playerSkillDataManager;
    private AudioSource[] buttonClips;

    private void Awake()
    {
        playerSkillDataManager = GameObject.Find("DataManager").GetComponent<PlayerSkillDataManager>();
        SetButtonSounds();
    }

    private void Start()
    {
        totalBriefsText.text = playerSkillDataManager.briefs.ToString();
        var sT = playerSkillTemplate.GetComponent<PlayerSkillTemplate>();

        foreach (var item in skillsInfo)
        {
            sT.title.text = item.title;
            sT.skillType.text = item.skillType;
            sT.description.text = item.description;
            sT.price.text = item.price.ToString();
            Instantiate(sT, TemplatesContainer.transform);
        }
    }

    private void OnEnable()
    {
        playerSkillDataManager.LoadData();
    }

    private void Update()
    {
        totalBriefsText.text = playerSkillDataManager.briefs.ToString();
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
