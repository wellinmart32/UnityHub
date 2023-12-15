using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public Text fruitsCollectedText;
    public Text gemsCollectedText;
    public Text briefsCollectedText;
    public Text lifesCollectedText;

    public int fruits;
    public int gems;
    public int briefs;
    public int lifes;

    public int currentHearts;
    public int currentActiveHearts;
    public int maxHearts;

    public GameObject[] hearts;

    private void Awake()
    {
        Transform heartsList = GameObject.Find("Hearts").transform;
        hearts = new GameObject[heartsList.childCount];

        for (int i = 0; i < heartsList.transform.childCount; i++)
        {
            hearts[i] = heartsList.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        fruitsCollectedText.text = fruits.ToString();
        gemsCollectedText.text = gems.ToString();
        briefsCollectedText.text = briefs.ToString();
        lifesCollectedText.text = lifes.ToString();
    }

    public void SetActiveHearts()
    {
        GameObject[] hearts = GameObject.Find("Player").GetComponent<PlayerRespawn>().hearts;

        for (int i = 0; i < maxHearts; i++)
        {
            if (i < currentActiveHearts)
            {
                hearts[i].tag = "ActiveHeart";
                hearts[i].SetActive(true);

                if (i < currentHearts)
                {
                    hearts[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    hearts[i].GetComponent<Image>().color = Color.black;
                }
            }
            else
            {
                hearts[i].tag = "Untagged";
                hearts[i].SetActive(false);
            }
        }
    }
}
